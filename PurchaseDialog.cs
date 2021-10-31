using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseDialog : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public CanvasGroup Berhasil;
    public CanvasGroup Gagal;

    public LevelPackWindow levelPackWindow;
    public LevelPack levelPack;

    public void Restart()
    {
        Berhasil.alpha = 0;
        Berhasil.blocksRaycasts = false;

        Gagal.alpha = 0;
        Gagal.blocksRaycasts = false;
    }

    public void TryToPurchase(LevelPackWindow packWindow)
    {
        levelPackWindow = packWindow;
        levelPack = levelPackWindow.levelPack;
        Appear();
    }

    public void Cancel()
    {
        Dissapear();
    }

    public void Purchase()
    {
        if (levelPack.CanBuy())
        {
            Berhasil.blocksRaycasts = true;
            Berhasil.LeanAlpha(1, 0.25f);
            JPanel.main.AddValue(-levelPack.cost);
            levelPack.Unlock();
        }
        else
        {
            Gagal.blocksRaycasts = true;
            Gagal.LeanAlpha(1, 0.25f);
        }
        levelPackWindow.Initialize();
        gameObject.LeanDelayedCall(2, () => Dissapear());
    }

    public void Appear()
    {
        gameObject.SetActive(true);
        Restart();
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
