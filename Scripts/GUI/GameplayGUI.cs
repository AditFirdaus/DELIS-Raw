using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayGUI : MonoBehaviour
{
    public static GameplayGUI main;

    [Header("References")]
    public TMP_Text RCombo;
    public TMP_Text RScore;
    public TMP_Text RName;
    public TMP_Text RMusic;
    public Image RBackground;

    [Header("Screens")]
    public PauseScreen SPause;
    public CountdownScreen SCountdown;
    public ResultScreen SResult;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        Debug.Log("Updated");
        Debug.Log(Gameplay.levelData);
        UpdateData();
        GameplayData.OnUpdateCombo.AddListener(UpdateCombo);
        GameplayData.OnUpdateScore.AddListener(UpdateScore);
    }

    public void UpdateCombo()
    {
        if (GameplayData._combo <= 3)
        {
            RCombo.text = "";
        }
        else
        {
            RCombo.text = GameplayData._combo.ToString();
        }


        RCombo.gameObject.LeanCancel();
        RCombo.gameObject.LeanScale(Vector3.one, 0);
        RCombo.gameObject.LeanScale(Vector3.one * 1.25f, 0.5f).setEasePunch();

    }
    public void UpdateScore()
    {
        RScore.text = string.Format("{0:000000}", ((int)GameplayData._score));

        RScore.gameObject.LeanCancel();
        RScore.gameObject.LeanScaleY(1, 0);
        RScore.gameObject.LeanScaleY(1.5f, 0.5f).setEasePunch();
    }

    public void UpdateData()
    {
        Gameplay.levelData.LoadData(true);
        RBackground.transform.localScale = Vector3.one * 1.25f;
        SetBackgroundSize(Vector2.one, 4, LeanTweenType.easeInOutExpo);
        RBackground.sprite = Gameplay.levelData.levelSprite;
        RName.text = Gameplay.levelData.levelName.ToString();
        RMusic.text = Gameplay.levelData.musicName.ToString();
    }

    public void SetBackgroundSize(Vector2 size, float time = 4, LeanTweenType leanTweenType = LeanTweenType.linear)
    {
        RBackground.gameObject.LeanCancel();
        RBackground.gameObject.LeanScale(size, time).setEase(leanTweenType).setIgnoreTimeScale(true);
    }
}