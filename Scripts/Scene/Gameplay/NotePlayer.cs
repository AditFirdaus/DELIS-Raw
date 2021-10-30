using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NotePlayer : MonoBehaviour
{
    public float spawnDistance = 100;
    public bool autoplay = true;
    public Transform noteArea;

    public NoteMap noteMap;
    public Note[] notes
    {
        get
        {
            return noteMap.Notes();
        }
    }
    public AudioSource audioSource;
    public NoteAnalyzerModule noteAnalyzerModule;

    public NoteAnalyzer noteAnalyzer;

    public void Start()
    {
        if (autoplay) Activate();
    }

    public void Activate()
    {
        noteAnalyzerModule.Init(audioSource, notes);
        noteAnalyzer.Init(this, audioSource, notes);
    }

    public void SpawnIndex(int i)
    {
        Spawn(notes[i]);
    }

    public void Spawn(Note note)
    {
        GameObject instance = GameObject.Instantiate(note.pack.register.instance, (Vector3)note.data.world + Vector3.forward * spawnDistance, Quaternion.identity, noteArea);
        gameObject.LeanMoveLocalZ(0, 1);

        GameNote gameNote = instance.GetComponent<GameNote>();
        if (gameNote)
        {
            gameNote.note = note;
        }

        if (Game.player.autohit) LeanTween.delayedCall(-note.pack.register.offset, () => GameplayInput.RayInput(Camera.main.ViewportToScreenPoint(note.data.viewport)));
    }

    public Note GetNote(int index = -1)
    {
        return notes[Mathf.Clamp(index, 0, notes.Length - 1)];
    }
}