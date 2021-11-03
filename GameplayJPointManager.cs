using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayJPointManager : MonoBehaviour
{
    public RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    public int highestCombo = 10;
    public int addHighest = 10;
    private void Awake()
    {
        GameplayData.OnUpdateCombo.AddListener(Validate);
    }

    public void Validate()
    {
        if (GameplayData._combo >= highestCombo)
        {
            Expand();
            JPanel.main.AddValue(Random.Range(15, 25));

            gameObject.LeanDelayedCall(2, () => Shrink());
            highestCombo += addHighest;
        }
    }

    public LTDescr Expand()
    {

        rectTransform.LeanCancel();
        canvasGroup.gameObject.LeanCancel();
        LTDescr tween = rectTransform.LeanMoveY(-25, 1).setEaseOutExpo();


        canvasGroup.LeanAlpha(1, 1).setEaseOutExpo();

        return tween;
    }

    public LTDescr Shrink()
    {

        rectTransform.LeanCancel();
        canvasGroup.gameObject.LeanCancel();
        LTDescr tween = rectTransform.LeanMoveY(75, 1).setEaseInExpo();


        canvasGroup.LeanAlpha(0, 1).setEaseInExpo();

        return tween;
    }

}
