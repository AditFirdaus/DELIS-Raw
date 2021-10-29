using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public CountdownScreen SCountdown;
    public RectTransform screen;
    public CanvasGroup backgroundCanvasGroup;
    public GameplayGUI gameplayGUI;

    public AudioClip pauseIn;
    public AudioClip pauseOut;


    private void OnApplicationPause(bool pauseStatus)
    {
        Pause();
    }

    public void Pause()
    {
        LeanAudio.play(pauseIn);
        if (Splash.main.buttonClick1) LeanAudio.play(Splash.main.buttonClick1);


        backgroundCanvasGroup.gameObject.LeanCancel();
        backgroundCanvasGroup.LeanAlpha(1, 0.5f).setIgnoreTimeScale(true);

        screen.LeanCancel();
        screen.LeanMoveX(0, 1).setEaseOutExpo().setIgnoreTimeScale(true);

        gameObject.SetActive(true);
        GameplayGUI.main.SetBackgroundSize(Vector2.one * 1.25f, 4, LeanTweenType.easeOutExpo);
        gameplayGUI.DisplayGameplayGUI(Vector2.one * 1.25f, 0, LeanTweenType.easeInBack);
        Gameplay.main.Freeze();
    }

    public void UnPause()
    {
        Gameplay.main.UnFreeze();
    }

    public void Resume()
    {
        LeanAudio.play(pauseOut);
        if (Splash.main.buttonClick1) LeanAudio.play(Splash.main.buttonClick1);

        backgroundCanvasGroup.gameObject.LeanCancel();
        backgroundCanvasGroup.LeanAlpha(0, 0.5f).setIgnoreTimeScale(true);

        screen.LeanCancel();
        screen.LeanMoveX(0, 1).setEaseOutExpo().setIgnoreTimeScale(true);


        screen.LeanCancel();
        screen.LeanMoveX(760, 1).setEaseOutExpo().setIgnoreTimeScale(true).setOnComplete(
            () =>
            {
                gameObject.SetActive(false);
            }
        );

        GameplayGUI.main.SetBackgroundSize(Vector2.one, 4, LeanTweenType.easeOutExpo);
        gameplayGUI.DisplayGameplayGUI(Vector2.one, 1, LeanTweenType.easeOutBack);
        SCountdown.Countdown(3, UnPause);
    }

}
