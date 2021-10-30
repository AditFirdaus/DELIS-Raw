using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    [Header("References")]
    public RectTransform localImageTransform;
    public Image image;
    public Image dim;
    public Image flash;

    [Header("Image")]
    public float ImageScaleS = 1;
    public float ImageScaleE = 1;

    public LTDescr SetDim(float value, float time = 0, LeanTweenType tweenType = LeanTweenType.linear)
    {
        dim.gameObject.LeanCancel();
        LTDescr tween =
        dim.gameObject.LeanValue(
            (float i) =>
            {
                Color after = dim.color;
                after.a = i;
                dim.color = after;
            },
            dim.color.a,
            value,
            time
        );

        tween.setEase(tweenType);
        tween.setIgnoreTimeScale(true);

        return tween;
    }

    public LTDescr Flash(float time = 1, LeanTweenType tweenType = LeanTweenType.easeOutExpo)
    {
        flash.gameObject.LeanCancel();
        LTDescr tween =
        flash.gameObject.LeanValue(
            (float i) =>
            {
                Color after = flash.color;
                after.a = i;

                flash.color = after;
            },
            1,
            0,
            time
        );

        tween.setEase(tweenType);
        tween.setIgnoreTimeScale(true);

        return tween;
    }

    public LTDescr ScaleImageAdvanced(float ScaleT, float time = 0, LeanTweenType leanTweenType = LeanTweenType.linear)
    {
        Vector2 from = image.rectTransform.localScale;
        Vector2 to = Vector2.one * ScaleT;

        return ScaleImage(from, to, time, leanTweenType);
    }

    // Image
    public LTDescr ExpandImage(float time = 1, LeanTweenType tweenType = LeanTweenType.linear)
    {
        Vector2 from = image.rectTransform.localScale;
        Vector2 to = Vector2.one * ImageScaleE;

        return ScaleImage(from, to, time, tweenType);
    }
    public LTDescr ShrinkImage(float time = 1, LeanTweenType tweenType = LeanTweenType.linear)
    {
        Vector2 from = image.rectTransform.localScale;
        Vector2 to = Vector2.one * ImageScaleS;

        return ScaleImage(from, to, time, tweenType);
    }


    // Base Function
    public LTDescr ScaleImage(Vector2 from, Vector2 to, float time = 0, LeanTweenType leanTweenType = LeanTweenType.linear)
    {
        RectTransform rectTransform = image.rectTransform;

        return Scale(image.rectTransform, from, to, time, leanTweenType);
    }


    public LTDescr Scale(RectTransform rectTransform, Vector2 from, Vector2 to, float time = 0, LeanTweenType tweenType = LeanTweenType.linear)
    {
        rectTransform.LeanCancel();

        rectTransform.localScale = from;
        LTDescr tween = rectTransform.LeanScale(to, time).setIgnoreTimeScale(true);

        tween.setEase(tweenType);
        tween.setIgnoreTimeScale(true);

        return tween;
    }
}
