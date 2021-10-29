using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayVisualizer : MonoBehaviour
{
    public LineRenderer lineRendererTop;
    public LineRenderer lineRendererBottom;

    public void Beat()
    {
        lineRendererTop.gameObject.LeanCancel();
        LeanTween.value(
            lineRendererTop.gameObject,
            (float i) =>
            {
                lineRendererTop.widthMultiplier = i;
            },
            0f,
            0.25f,
            1f
        ).setEasePunch();

        lineRendererBottom.gameObject.LeanCancel();
        LeanTween.value(
            lineRendererBottom.gameObject,
            (float i) =>
            {
                lineRendererBottom.widthMultiplier = i;
            },
            0f,
            0.25f,
            1f
        ).setEasePunch();
    }
}
