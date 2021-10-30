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
    public static GameplayStats gameplayStats;

    public static float delay = 0;
    public static Gameplay main;
    public static LevelData levelData;
    public LevelData _levelData;
    public AudioSource audioSource;
    public NotePlayer notePlayer;

    public GameplayGUI gameplayGUI;

    public NoteRegister[] noteRegisters;

    public LTDescr levelCompleteLT;


    private void Awake()
    {
        main = this;
        gameplayGUI.transform.localScale = Vector3.one * 1.5f;
        gameplayGUI.canvasGroup.alpha = 0;

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
        Player.Load(Game.player);

        GameplayData.Reset();

        if (levelData == null) levelData = _levelData;

        gameplayGUI.DisplayGameplayGUI(Vector2.one, 1, LeanTweenType.easeOutBack);
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

        notePlayer.Activate();

        Score.totalNotes = levelData.noteMap.TotalNotes();

        GameplayGUI.countdownScreen.Countdown(4, LevelBegin);
    }

    public void LevelBegin()
    {
        UnFreeze();
        audioSource.Play();
        notePlayer.Activate();
    }
    public void LevelEnd()
    {
        gameplayGUI.canvasGroup.interactable = false;
        gameplayGUI.gameObject.LeanCancel();
        gameplayGUI.canvasGroup.LeanAlpha(0, 3).setIgnoreTimeScale(true);
        gameplayGUI.GetComponent<RectTransform>().LeanScale(Vector2.one * 1.25f, 3).setIgnoreTimeScale(true).setEaseInBack().setOnComplete(
            () =>
            {
                GameplayGUI.main.backgroundManager.ExpandImage(3, LeanTweenType.easeInOutCubic);

                levelCompleteLT = LeanTween.delayedCall(3, () =>
                {
                    audioSource.Stop();
                    GameplayGUI.resultScreen.Result();
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



}