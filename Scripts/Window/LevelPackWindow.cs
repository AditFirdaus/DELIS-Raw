using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class LevelPackWindow : MonoBehaviour
{
    public bool expand = false;

    public RectTransform window;

    public LevelPackSelector levelPackSelector;
    public LevelPack levelPack;

    public TMP_Text title;
    public TMP_Text cost;
    public Button enterButton;
    public Button buyButton;

    public LevelDataDisplayer levelDataDisplayer;

    // Set Get

    public LayoutGroup layoutGroup
    {
        get
        {
            return levelPackSelector.levelPackLayoutGroup;
        }
    }

    public ContentSizeFitter contentFitter
    {
        get
        {
            return levelPackSelector.contentSizeFitter;
        }
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        levelPack.LoadData();

        title.text = levelPack.name;
        cost.text = levelPack.cost.ToString();
        buyButton.onClick.AddListener(Buy);

        levelDataDisplayer.levelPack = levelPack;
        levelDataDisplayer.CreateLevelDataPanels();

        enterButton.gameObject.SetActive(levelPack.data.purchased);
        buyButton.gameObject.SetActive(!levelPack.data.purchased);
    }

    public void Buy()
    {
        levelPack.Buy();
        Initialize();
        JPanel.main.UpdatePanel();
    }

    public void Select()
    {
        MainMenu.main.Select(levelPack);
    }

    public void Enter()
    {
        MainMenu.main.Enter();
    }

    public void ToggleExpand()
    {
        expand = !expand;
        if (expand)
        {
            ExpandWindow();
        }
        else
        {
            ShrinkWindow();
        }
    }

    public void ExpandWindow()
    {
        levelDataDisplayer.HideLevelDataPanels();
        levelPackSelector.ShrinkWindow();

        window.LeanCancel();
        Vector2 targetSize = window.rect.size;
        targetSize.x = 750;

        window.LeanSize(targetSize, 0.5f).setEaseOutExpo().setOnUpdateVector2(
            (Vector2 currentSize) =>
            {
                contentFitter.SetLayoutHorizontal();
                contentFitter.SetLayoutVertical();
                layoutGroup.CalculateLayoutInputHorizontal();
                layoutGroup.SetLayoutHorizontal();
            }
        );

        levelDataDisplayer.ExpandLevelDataPanels();

        expand = true;
    }

    public void ShrinkWindow()
    {
        window.LeanCancel();
        Vector2 targetSize = window.rect.size;
        targetSize.x = 250;

        window.LeanSize(targetSize, 0.5f).setEaseOutExpo().setOnUpdateVector2(
            (Vector2 currentSize) =>
            {
                contentFitter.SetLayoutHorizontal();
                contentFitter.SetLayoutVertical();
                layoutGroup.CalculateLayoutInputHorizontal();
                layoutGroup.SetLayoutHorizontal();

            }
        );

        expand = false;

    }
}
