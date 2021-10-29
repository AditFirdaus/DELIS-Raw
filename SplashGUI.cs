using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashGUI : MonoBehaviour
{
    public AudioClip introMusic;
    public CanvasGroup SMK;
    public CanvasGroup KKSI;
    public CanvasGroup Title;
    public Button touchButton;

    private void Start()
    {
        Time.timeScale = 1;
        LeanTween.delayedCall(3,
        () =>
        {
            LeanAudio.play(introMusic);
            SMKScreen();
        }
        );


    }

    public void SMKScreen()
    {
        SMK.LeanAlpha(1, 1).setOnComplete(
            () =>
            {
                SMK.LeanAlpha(0, 0.75f).setDelay(0.25f).setOnComplete(
                    () =>
                    {
                        KKSIScreen();
                    }
                ).setEaseInSine();
            }
        ).setEaseOutSine();
    }
    public void KKSIScreen()
    {
        KKSI.LeanAlpha(1, 1).setDelay(1.5f).setOnComplete(
            () =>
            {
                KKSI.LeanAlpha(0, 0.75f).setDelay(0.5f).setOnComplete(
                    () =>
                    {
                        TitleScreen();
                    }
                ).setEaseInSine();
            }
        ).setEaseOutSine();
    }
    public void TitleScreen()
    {
        Title.LeanAlpha(1, 0.5f).setDelay(1).setOnComplete(
            () =>
            {
                Title.interactable = true;
                touchButton.interactable = true;
            }
        );
    }

}
