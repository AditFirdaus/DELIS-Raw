using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialPhase : MonoBehaviour
{
    public bool canProceed;
    public bool canEnd;
    public CanvasGroup canvasGroup;
    public float inTime;
    public float fadein;
    public float duration;
    public float fadeout;
    public float outTime;

    public UnityEvent start;
    public UnityEvent active;
    public UnityEvent end;

    [ContextMenu("Get Canvas")]
    private void GetCanvas()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Proceed()
    {
        canProceed = true;
    }

    public void End()
    {
        canEnd = true;
    }

    public IEnumerator Activate()
    {
        while (!canProceed) yield return null;

        start.Invoke();

        yield return new WaitForSeconds(inTime);

        canvasGroup.LeanAlpha(1, fadein);

        yield return new WaitForSeconds(fadein);

        active.Invoke();

        while (!canEnd) yield return null;
        yield return new WaitForSeconds(duration);

        canvasGroup.LeanAlpha(0, fadeout);

        yield return new WaitForSeconds(fadeout);

        end.Invoke();

        yield return new WaitForSeconds(outTime);
    }

}