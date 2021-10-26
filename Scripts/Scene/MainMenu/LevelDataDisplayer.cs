using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataDisplayer : MonoBehaviour
{
    public LevelPack levelPack;
    public GameObject levelDataPanelPrefab;
    public LevelDataPanel[] levelDataPanels;
    public GameObject levelDataSelectionGrid;
    public AudioClip panelAudio;

    [ContextMenu("Create Windows")]
    public void CreateLevelDataPanels()
    {
        foreach (LevelDataPanel window in levelDataPanels)
        {
            Destroy(window.gameObject);
        }

        LevelData[] levelDatas = levelPack.levelDatas;
        levelDataPanels = new LevelDataPanel[levelDatas.Length];

        for (int i = 0; i < levelDatas.Length; i++)
        {
            GameObject window = Instantiate(levelDataPanelPrefab, levelDataSelectionGrid.transform);
            LevelDataPanel levelDataPanel = window.GetComponent<LevelDataPanel>();
            levelDataPanel.levelData = levelDatas[i];

            levelDataPanel.Initialize();

            levelDataPanels[i] = levelDataPanel;
        }
    }

    public void HideLevelDataPanels()
    {
        foreach (LevelDataPanel panel in levelDataPanels)
        {
            panel.gameObject.LeanCancel();
            panel.canvasGroup.alpha = 0;
            panel.transform.localScale = Vector3.one * 0.5f;
        }
    }

    public void ExpandLevelDataPanels()
    {
        HideLevelDataPanels();
        LTSeq sequence = LeanTween.sequence();
        foreach (LevelDataPanel panel in levelDataPanels)
        {
            LTDescr lTDescr = LeanTween.delayedCall(0.05f,
                () =>
                {
                    LeanAudio.play(panelAudio);
                    panel.canvasGroup.LeanAlpha(1, 0.5f).setEaseOutExpo();
                    panel.gameObject.LeanScale(Vector3.one, 2f).setEaseOutElastic();
                }
            );

            sequence.append(lTDescr);
        }
    }
}
