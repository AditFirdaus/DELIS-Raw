using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayIndicator : MonoBehaviour
{
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

        gameObject.LeanCancel();
        gameObject.LeanMove(note.data.world, note.data.time - audioSource.time).setEase(easeType);
    }

}
