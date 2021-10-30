using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static LevelPack levelPack;
    public static MainMenu main;
    public GameObject loadingScreen;
    public static LevelPack[] levelPacks;

    private void Awake()
    {
        main = this;
    }

    public static void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Start()
    {
        levelPacks = LevelPackExtensions.LoadLevelPacks();

        LoadingScreen.ScreenPrefab = loadingScreen;

        Player.Load(Game.player);

        JPanel.main.UpdatePanel();
        //PlayerPanel.main.UpdatePanel();

        LevelPackSelector.main.CreateLevelPackWindows();
    }

    public void Select(LevelPack pack)
    {
        MainMenuGUI.main.SetLevelPackBackground(pack);
        levelPack = pack;
    }

    public void Enter(LevelPack pack = null)
    {
        if (pack) levelPack = pack;

        if (!levelPack) return;

        LoadingScreen.Load(() => LevelSelector.Enter(levelPack));
    }

    public void Exit()
    {
        Application.Quit();
    }

}
