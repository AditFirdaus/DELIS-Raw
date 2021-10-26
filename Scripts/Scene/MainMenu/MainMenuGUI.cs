using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuGUI : MonoBehaviour
{
    public static MainMenuGUI main;

    [Header("Datas")]
    public Sprite _background;

    [Header("References")]
    public Image R_background;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        LevelPack levelPack = MainMenu.levelPacks[Random.Range(0, MainMenu.levelPacks.Length)];
        LevelData levelData = levelPack.levelDatas[Random.Range(0, levelPack.levelDatas.Length)];

        levelData.LoadSprite();
        _background = levelData.levelSprite;

        R_background.sprite = _background;
    }

    public void Exit()
    {
        Application.Quit();
    }

}

