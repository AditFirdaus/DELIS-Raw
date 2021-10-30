using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class TutorialNoteHandler : MonoBehaviour
{
    public GameObject noteObject;
    public AudioClip hitSound;
    public int loop = 0;
    public Data[] datas;

    public UnityEvent onComplete = new UnityEvent();

    public void Activate()
    {
        StartCoroutine(PlayNotes());
    }

    public IEnumerator PlayNotes()
    {
        for (int i = 0; i < loop; i++)
        {
            foreach (Data data in datas)
            {
                yield return StartCoroutine(PlayNote(data));
            }
        }
        onComplete.Invoke();
    }

    public IEnumerator PlayNote(Data data)
    {
        data.complete = false;
        yield return new WaitForSeconds(data.inDelay);

        Spawn(data);

        while (!data.complete) yield return null;

        yield return new WaitForSeconds(data.outDelay);
    }

    public void Spawn(Data data)
    {
        Transform noteContainer = Tutorial.noteArea;
        Vector2 worldPosition = Camera.main.ViewportToWorldPoint(data.viewportPosition);
        GameObject noteInstance = GameObject.Instantiate(noteObject, worldPosition, Quaternion.identity, noteContainer);

        GameNote gameNote = noteInstance.GetComponent<GameNote>();
        if (gameNote)
        {
            gameNote.onHit.AddListener(
                (Note note) =>
                {
                    LeanAudio.play(hitSound);
                }
            );

            gameNote.onEnd.AddListener(
                (Note note) =>
                {
                    data.complete = true;
                }
            );
        }
    }

    [System.Serializable]
    public class Data
    {
        public bool complete;
        public Vector2 viewportPosition;
        public float inDelay = 1;
        public float outDelay = 1;
    }
}