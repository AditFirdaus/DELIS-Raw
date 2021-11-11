using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameNote : MonoBehaviour
{
    public bool autostart = true;

    [Header("Properties")]
    public Note note;
    public GameObject stateIndicator;
    public NoteState state = NoteState.Miss;
    public Animator animator;

    [Header("Events")]
    public UnityEvent<Note> onStart = new UnityEvent<Note>();
    public UnityEvent<Note> onHit = new UnityEvent<Note>();
    public UnityEvent<Note> onEnd = new UnityEvent<Note>();

    private void Start()
    {
        if (autostart) Play();
    }

    public void Play()
    {
        animator.Play("Note");
        onStart.Invoke(note);
    }

    public void Beat()
    {
        if (Game.player.autohit) Hit();
    }

    public void Hit()
    {
        animator.Play("Hit");
        ValidateState();
        onHit.Invoke(note);
    }

    public void ValidateState()
    {
        GameplayData.Validate(state);
        SpawnIndicator();
    }

    public void SpawnIndicator()
    {
        StateIndicator indicator = Instantiate(stateIndicator, transform.position, transform.rotation, transform.parent).GetComponent<StateIndicator>();
        indicator.Indicate(state);
    }

    public void End()
    {
        Destroy(gameObject);
        onEnd.Invoke(note);
    }

}