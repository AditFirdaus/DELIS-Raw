using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NotePlayer : MonoBehaviour
{
    public float spawnDistance = 100;
    public bool autoplay = true;
    public bool autohit = false;
    public Transform noteArea;

    public NoteMap noteMap;
    public AudioSource audioSource;

    public NoteAnalyzer noteAnalyzer;

    public void Start()
    {
        if (Game.player.autohit) autohit = Game.player.autohit;
        if (autoplay) Activate();
    }

    public void Activate()
    {
        noteAnalyzer.notes = noteMap.Notes();

        noteAnalyzer.analyzing = true;
        noteAnalyzer.Activate();
    }

    public void Spawn()
    {
        Note note = noteAnalyzer.notes[noteAnalyzer.index];
        GameObject gameObject = GameObject.Instantiate(note.pack.register.instance, (Vector3)note.data.world + Vector3.forward * spawnDistance, Quaternion.identity, noteArea);
        gameObject.LeanMoveLocalZ(0, 1);

        if (autohit) LeanTween.delayedCall(-note.pack.register.offset, () => GameplayInput.RayInput(Camera.main.ViewportToScreenPoint(note.data.viewport)));
    }
}