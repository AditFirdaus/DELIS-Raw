using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayIndicator : MonoBehaviour
{
    public float intensity = 0;
    public float distance = 0;
    public float time = 0;
    public LeanTweenType easeType = LeanTweenType.easeOutExpo;
    public GameObject Navigator;
    public AudioSource audioSource;
    public NotePlayer notePlayer;
    public void MoveToNoteIndex(int i)
    {
        MoveToNote(notePlayer.GetNote(i + 1));
    }
    public void MoveToNote(Note note)
    {
        CalculateIntensity(note);

        gameObject.LeanCancel();
        gameObject.LeanMove(note.data.world, note.data.time - audioSource.time).setEase(easeType);
    }

    public void CalculateIntensity(Note note)
    {
        distance = Vector2.Distance(
            gameObject.transform.position,
            note.data.world
        );

        time = note.data.time - audioSource.time;

        intensity = distance / time;
    }

}
