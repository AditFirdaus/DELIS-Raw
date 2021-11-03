using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour
{
    public static Splash main;

    [Header("Tutorial")]
    public LevelPack levelPack;
    public LevelData levelData;
    [Header("Assets")]
    public GameObject loadingScreenPrefab;

    public AudioClip buttonClick1;
    public AudioClip buttonClick2;
    public AudioClip buttonClick3;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        Player.Load(Game.player);
        InitializeAssets();
    }
    void InitializeAssets()
    {
        LoadingScreen.ScreenPrefab = loadingScreenPrefab;
    }
    public void Play()
    {
        if (Game.player.tutorial) LoadingScreen.Load(() => Tutorial.PlayTutorial());
        else LoadingScreen.Load(() => MainMenu.Menu());
    }

}
