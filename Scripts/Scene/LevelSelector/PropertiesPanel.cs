using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesPanel : MonoBehaviour
{
    public static PropertiesPanel main;
    public Image RBackground;

    [Header("Data Reference")]
    public TMP_Text Rname;
    public TMP_Text Rmusic;
    public TMP_Text Rdescription;
    public TMP_Text Rsource;

    public RectTransform RPDescription;
    public CanvasGroup statsPanel;

    public AudioSource audioSource;

    private void Awake()
    {
        main = this;
    }

    public void UpdatePanel(LevelData levelData)
    {
        levelData.LoadData(true);
        PlayAudio(levelData.musicClip);

        RBackground.gameObject.LeanCancel();
        RBackground.transform.localScale = Vector3.one * 1.5f;
        RBackground.gameObject.LeanScale(Vector3.one * 1.25f, 1f).setEaseOutExpo();

        Rname.text = levelData.levelName;
        Rmusic.text = levelData.musicName;
        Rdescription.text = levelData.descriptions;
        Rsource.text = levelData.source;
        RBackground.sprite = levelData.levelSprite;

        AnimatePanels();
    }

    public void PlayAudio(AudioClip audioClip)
    {
        if (audioSource.isPlaying is false) audioSource.Play();
        float time = audioSource.time;

        audioSource.Pause();
        audioSource.clip = audioClip;
        audioSource.time = time % audioClip.length;

        audioSource.UnPause();
        if (audioSource.isPlaying is false) audioSource.Play();
    }

    public void AnimatePanels()
    {
        Rsource.rectTransform.LeanCancel();
        Rsource.rectTransform.LeanSetLocalPosX(-100);
        Rsource.rectTransform.LeanMoveLocalX(25, 1).setEaseOutExpo();

        Rmusic.rectTransform.LeanCancel();
        Rmusic.rectTransform.LeanSetLocalPosX(-200);
        Rmusic.rectTransform.LeanMoveLocalX(-0, 1).setEaseOutExpo();

        Rname.rectTransform.LeanCancel();
        Rname.rectTransform.LeanSetLocalPosX(-100);
        Rname.rectTransform.LeanMoveLocalX(-25, 1).setEaseOutExpo();




        RPDescription.LeanCancel();
        RPDescription.LeanSize(new Vector2(0, RPDescription.sizeDelta.y), 0);
        RPDescription.LeanSize(new Vector2(550, RPDescription.sizeDelta.y), 1f).setEaseOutExpo();

        Rdescription.gameObject.LeanCancel();
        Rdescription.gameObject.LeanValue((Vector2 v) => Rdescription.rectTransform.anchorMin = v, new Vector2(1, 1), new Vector2(1, 0), 1f).setEaseOutExpo();


        RectTransform statsPanelTransform = statsPanel.GetComponent<RectTransform>();

        statsPanelTransform.LeanCancel();
        statsPanel.gameObject.LeanCancel();

        statsPanelTransform.LeanScale(Vector3.one * 0.75f, 0);
        statsPanelTransform.LeanScale(Vector3.one, 1f).setEaseOutExpo();


        statsPanel.alpha = 0;
        statsPanel.LeanAlpha(1, 0.25f);




    }

}
