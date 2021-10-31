using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDialog : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public void TryToExit()
    {
        Appear();
    }

    public void Cancel()
    {
        Dissapear();
    }

    public void Exit()
    {
        DialogManager.main.Blocker.SetActive(false);
        gameObject.SetActive(false);
        Application.Quit();
        Debug.Log("Exited");
    }

    public void Appear()
    {
        gameObject.SetActive(true);
        DialogManager.main.Blocker.SetActive(true);

        canvasGroup.gameObject.LeanCancel();

        canvasGroup.alpha = 0;
        canvasGroup.LeanAlpha(1, 0.25f);

        transform.localScale = Vector3.one * 0.9f;
        gameObject.LeanScale(Vector3.one, 0.5f).setEaseOutExpo();
    }

    public void Dissapear()
    {
        DialogManager.main.Blocker.SetActive(false);

        canvasGroup.gameObject.LeanCancel();

        canvasGroup.LeanAlpha(0, 0.25f);

        gameObject.LeanScale(Vector3.one * 0.9f, 0.5f).setEaseOutExpo().setOnComplete(
            () =>
            {
                gameObject.SetActive(false);
            }
        );
    }

}
