using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateIndicator : MonoBehaviour
{
    public Animator animator;
    public void Indicate(NoteState noteState)
    {
        animator.Play(noteState.ToString());
    }
    public void End()
    {
        Destroy(gameObject);
    }
}
