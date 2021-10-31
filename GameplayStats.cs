using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayStats : MonoBehaviour
{
    public TMP_Text RCombo;
    public TMP_Text RScore;

    private void Awake()
    {
        GameplayData.OnUpdateCombo.AddListener(UpdateCombo);
        GameplayData.OnUpdateScore.AddListener(UpdateScore);
    }

    public void UpdateCombo()
    {
        if (GameplayData._combo < 3)
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

}
