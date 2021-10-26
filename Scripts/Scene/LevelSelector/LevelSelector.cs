using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public static LevelSelector main;
    public static LevelPack levelPack;
    public static LevelData levelData;
    public LevelPack _levelPack;
    public CanvasGroup levelSelector;
    public AudioSource audioSource;

    public static void Enter(LevelPack levelPack)
    {
        LevelSelector.levelPack = levelPack;
        SceneManager.LoadScene("LevelSelector");
    }

    private void Awake()
    {
        main = this;
        if (levelPack is null) levelPack = _levelPack;
    }

    private void Start()
    {
        levelPack.LoadMusics();
        LevelDataSelector.main.CreateLevelDataWindows();

        if (levelPack.levelDatas.Contains(levelData)) Select(levelData);
        else Select(levelPack.levelDatas[0]);
    }

    public void Select(LevelData level)
    {
        levelData = level;
        PropertiesPanel.main.UpdatePanel(level);
        UserStats.main.UpdateStats(level);
    }

    public void Play()
    {
        LeanTween.value(audioSource.gameObject,
            (float i) =>
            {
                audioSource.volume = i;
            },
            audioSource.volume,
            0,
            1
        ).setEaseOutExpo();
        levelSelector.gameObject.LeanScale(Vector3.one * 1.5f, 1);
        levelSelector.LeanAlpha(0, 1).setEaseOutExpo()
        .setOnComplete(
            () =>
            {
                if (Game.player.creator)
                {
                    NoteMapCreator.Create(levelData);
                }
                else
                {
                    Gameplay.Play(levelData);
                }
            }
        );

        /*
        LoadingScreen.Load(() =>
        {
            
        });
        */
    }

    public void Back()
    {
        LoadingScreen.Load(() => MainMenu.Menu());
    }
}
