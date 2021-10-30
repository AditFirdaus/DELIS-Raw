using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuGUI : MonoBehaviour
{
    public static MainMenuGUI main;

    [Header("References")]
    public BackgroundManager backgroundManager;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        UpdateUI();
        RandomizeBackground();
    }

    public void UpdateUI()
    {

    }

    public void RandomizeBackground()
    {
        LevelPack levelPack = MainMenu.levelPacks[Random.Range(0, MainMenu.levelPacks.Length)];

        SetLevelPackBackground(levelPack);
    }

    public void SetLevelDataBackground(LevelData levelData)
    {
        levelData.LoadSprite();
        SetBackground(levelData.levelSprite);
    }

    public void SetBackground(Sprite sprite)
    {
        backgroundManager.image.sprite = sprite;
        backgroundManager.Flash();
        backgroundManager.image.rectTransform.localScale = backgroundManager.image.rectTransform.localScale * 2;
        backgroundManager.ShrinkImage(1f, LeanTweenType.easeOutExpo);
    }

    public void SetLevelPackBackground(LevelPack levelPack)
    {
        LevelData levelData = levelPack.levelDatas[Random.Range(0, levelPack.levelDatas.Length)];
        SetLevelDataBackground(levelData);
    }

    public void Exit()
    {
        Application.Quit();
    }

}

