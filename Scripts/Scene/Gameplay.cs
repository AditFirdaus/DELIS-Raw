using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Gameplay : MonoBehaviour
{
    public static float delay = 0;
    public static Gameplay main;
    public static LevelData levelData;
    public LevelData _levelData;
    public AudioSource audioSource;
    public NotePlayer notePlayer;

    public CanvasGroup gameplayGUI;

    public NoteRegister[] noteRegisters;

    public LTDescr levelCompleteLT;


    private void Awake()
    {
        main = this;
        gameplayGUI.transform.localScale = Vector3.one * 1.5f;
        gameplayGUI.alpha = 0;

        if (levelData == null) levelData = _levelData;
    }

    public static void Play(LevelData level, float delay = -1)
    {
        if (delay < 0) delay = LoadingScreen.processDuration / 2;
        Gameplay.delay = delay;
        Gameplay.levelData = level;
        SceneManager.LoadScene("Gameplay");
    }

    private void Start()
    {

        GameplayData.Reset();

        if (levelData == null) levelData = _levelData;



        Debug.Log(levelData);
        Debug.Log(_levelData);
        DisplayGameplayGUI(Vector2.one, 1, LeanTweenType.easeOutBack);
        LeanTween.delayedCall(
            delay,
            StartGame
        );
    }

    public void StartGame()
    {
        NoteRegister.registers = noteRegisters;

        levelData.LoadData(true);

        audioSource.clip = levelData.musicClip;
        notePlayer.noteMap = levelData.noteMap;

        Score.totalNotes = levelData.noteMap.TotalNotes();

        if (Game.player.autohit) notePlayer.autohit = true;


        GameplayGUI.main.SCountdown.Countdown(4, LevelBegin);
    }

    public void LevelBegin()
    {
        UnFreeze();
        audioSource.Play();
        notePlayer.Activate();
    }
    public void LevelEnd()
    {
        gameplayGUI.interactable = false;
        gameplayGUI.gameObject.LeanCancel();
        gameplayGUI.LeanAlpha(0, 3).setIgnoreTimeScale(true);
        gameplayGUI.GetComponent<RectTransform>().LeanScale(Vector2.one * 1.25f, 3).setIgnoreTimeScale(true).setEaseInBack().setOnComplete(
            () =>
            {
                GameplayGUI.main.SetBackgroundSize(Vector2.one * 1.25f, 3, LeanTweenType.easeInOutCubic);

                levelCompleteLT = LeanTween.delayedCall(3, () =>
                {
                    audioSource.Stop();
                    GameplayGUI.main.SResult.Result();
                }).setIgnoreTimeScale(true);
            }
        );

    }
    public void Freeze()
    {
        audioSource.Pause();
        Time.timeScale = 0;
    }
    public void UnFreeze()
    {
        audioSource.UnPause();
        Time.timeScale = 1;
    }

    public void Restart()
    {
        if (Splash.main.buttonClick1) LeanAudio.play(Splash.main.buttonClick1);
        Time.timeScale = 1;
        Play(levelData);
        //LoadingScreen.Load(() => );
    }
    public void Back()
    {
        if (Splash.main.buttonClick1) LeanAudio.play(Splash.main.buttonClick1);
        if (levelCompleteLT != null) LeanTween.cancel(levelCompleteLT.id);


        LevelPack levelPack = LevelSelector.levelPack;

        Time.timeScale = 1;
        if (levelPack) LoadingScreen.Load(() =>
        {

            levelPack.LoadMusics();

            LevelSelector.Enter(levelPack);

        });
    }

    public void DisplayGameplayGUI(Vector2 size, float alpha, LeanTweenType leanTweenType = LeanTweenType.linear)
    {
        gameplayGUI.gameObject.LeanCancel();
        gameplayGUI.gameObject.LeanScale(size, 1).setEase(leanTweenType).setIgnoreTimeScale(true);
        gameplayGUI.LeanAlpha(alpha, 1f).setIgnoreTimeScale(true);
    }

}