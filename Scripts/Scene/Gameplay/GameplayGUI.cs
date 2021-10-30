using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameplayGUI : MonoBehaviour
{
    public static GameplayGUI main;
    public static CountdownScreen countdownScreen;
    public static PauseScreen pauseScreen;
    public static ResultScreen resultScreen;

    [Header("Screens")]
    public CountdownScreen _countdownScreen;
    public PauseScreen _pauseScreen;
    public ResultScreen _resultScreen;


    [Header("References")]
    public CanvasGroup canvasGroup;
    public TMP_Text RName;
    public TMP_Text RMusic;
    public BackgroundManager backgroundManager;

    private void Awake()
    {
        main = this;
        countdownScreen = _countdownScreen;
        pauseScreen = _pauseScreen;
        resultScreen = _resultScreen;
    }

    private void Start()
    {
        UpdateData();
    }

    public void UpdateData()
    {
        Gameplay.levelData.LoadData(true);
        backgroundManager.image.transform.localScale = Vector3.one * 1.25f;
        backgroundManager.ShrinkImage(4, LeanTweenType.easeInOutExpo);
        backgroundManager.image.sprite = Gameplay.levelData.levelSprite;
        RName.text = Gameplay.levelData.levelName.ToString();
        RMusic.text = Gameplay.levelData.musicName.ToString();
    }

    public void DisplayGameplayGUI(Vector2 size, float alpha, LeanTweenType leanTweenType = LeanTweenType.linear)
    {
        gameObject.LeanCancel();
        gameObject.LeanScale(size, 1).setEase(leanTweenType).setIgnoreTimeScale(true);
        canvasGroup.LeanAlpha(alpha, 1f).setIgnoreTimeScale(true);
    }
}