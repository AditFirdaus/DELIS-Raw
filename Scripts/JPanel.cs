using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class JPanel : MonoBehaviour
{
    public static JPanel main;
    public int currentValue;
    public int totalAmount;
    public TMP_Text JPoint;
    public CanvasGroup RFlash;
    public RectTransform ChangeTransform;
    public CanvasGroup ChangeCanvas;
    public TMP_Text Change;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        UpdatePanel();
        Debug.Log("Updated");
    }

    public LTDescr FlashPanel()
    {
        RFlash.alpha = 1;
        return RFlash.LeanAlpha(0, 0.25f).setEaseOutExpo();
    }

    public void UpdatePanel()
    {
        Player.Load(Game.player);
        currentValue = Game.player.jPoint;
        JPoint.text = Game.player.jPoint.ToString();
    }

    public LTDescr AddValue(int amount)
    {
        FlashPanel();
        int jPoint = Game.player.jPoint;
        Game.player.jPoint += amount;
        int jPointAfter = Game.player.jPoint;

        Game.player.Save();

        totalAmount += amount;

        Change.text = totalAmount.ToString();


        LTDescr tween = ExpandValue().setOnComplete(
            () =>
            {
                TweenTextValue(
                    JPoint, // Global
                    jPoint,
                    jPointAfter,
                    1
                    ).setOnComplete(
                        () => JPoint.text = jPointAfter.ToString()
                    );

                TweenTextValue(
                    Change, // Global
                    totalAmount,
                    0,
                    1
                    ).setOnComplete(
                        () =>
                        {
                            totalAmount = 0;
                            Change.text = "0";

                            ShrinkValue();
                        }
                    );
            }
        );

        return tween;
    }

    public LTDescr ExpandValue()
    {
        RectTransform rectTransform = ChangeTransform;

        rectTransform.LeanCancel();
        ChangeCanvas.gameObject.LeanCancel();
        LTDescr tween = rectTransform.LeanMoveLocalX(-125, 1).setEaseOutExpo();


        ChangeCanvas.LeanAlpha(1, 1).setEaseOutExpo();

        return tween;
    }

    public LTDescr ShrinkValue()
    {
        RectTransform rectTransform = ChangeTransform;

        rectTransform.LeanCancel();
        ChangeCanvas.gameObject.LeanCancel();
        LTDescr tween = rectTransform.LeanMoveLocalX(-250, 1).setEaseInExpo();


        ChangeCanvas.LeanAlpha(0, 1).setEaseInExpo();

        return tween;
    }

    public LTDescr TweenTextValue(TMP_Text textField, int from, int to, float time)
    {
        LTDescr tween = LeanTween.value(
            gameObject,
            (float i) =>
            {
                int value = Mathf.RoundToInt(i);
                textField.text = value.ToString();
            },
            from,
            to,
            time
        );

        tween.setEaseOutExpo();

        return tween;
    }

}
