using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public static GameObject ScreenPrefab;
    [NonSerialized] public GameObject screen;


    [Header("Loading Screen")]

    public float introDuration = 1;
    public static float processDuration = 3; public float slicedProcess { get { return processDuration / 2; } }
    public float outroDuration = 1;
    public string[] gameTips;

    public float loadingOffset
    {
        get
        {
            return introDuration + slicedProcess;
        }
    }

    [Header("References")]
    public CanvasGroup screenCanvas;
    public CanvasGroup tipsCanvas;
    public CanvasGroup indicatorCanvas;
    public CanvasGroup loadingReferences;
    public Image backgroundPanel;


    [Header("Data References")]
    public TMP_Text tipsText;



    public static void Load(Action targetAction)
    {
        Debug.Log("Here");
        GameObject window = Instantiate(ScreenPrefab);
        DontDestroyOnLoad(window);

        Debug.Log("Here");
        LoadingScreen loadingScreen = window.GetComponent<LoadingScreen>();

        Debug.Log("Here");
        loadingScreen.screen = window;
        loadingScreen.Loading(targetAction);
    }

    public void Loading(Action action)
    {
        StartCoroutine(LoadingProcess(action, processDuration, introDuration, outroDuration));

    }

    public IEnumerator LoadingProcess(Action action, float processDuration = 0, float introDuration = 0, float outroDuration = 0)
    {
        float slicedProcess = processDuration / 2;

        Intro();
        yield return new WaitForSecondsRealtime(introDuration);
        Debug.Log("Here");
        ProcessStart();
        yield return new WaitForSecondsRealtime(slicedProcess);
        Debug.Log("Here");
        yield return StartCoroutine(Process(action));
        Debug.Log("Here");
        ProcessEnd();
        yield return new WaitForSecondsRealtime(slicedProcess);
        Debug.Log("Here");
        Outro();
        yield return new WaitForSecondsRealtime(outroDuration);
        Destroy(gameObject);
    }

    public void Intro()
    {
        AssignTips();


        screenCanvas.alpha = 0;
        screenCanvas.LeanAlpha(1, introDuration / 2).setEaseOutSine();
    }

    public void Outro()
    {
        screenCanvas.LeanAlpha(0, outroDuration / 2).setEaseInSine();
    }

    public void ProcessStart()
    {
        // Indicator Canvas
        //indicatorCanvas.alpha = 0;
        //indicatorCanvas.LeanAlpha(1, slicedProcess / 2).setDelay(slicedProcess / 2);

        loadingReferences.LeanAlpha(1, slicedProcess / 4).setDelay(slicedProcess / 2).setEaseInExpo();

        // Background Panel
        backgroundPanel.fillAmount = 0;
        backgroundPanel.fillOrigin = 0;
        LeanTween.value(backgroundPanel.gameObject, (float i) => backgroundPanel.fillAmount = i, 0, 1.25f, slicedProcess / 2).setEaseInOutQuart().setDelay(slicedProcess / 2);
    }

    public void ProcessEnd()
    {
        // Indicator Canvas
        // indicatorCanvas.LeanAlpha(0, slicedProcess / 2).setDelay(slicedProcess / 2);


        loadingReferences.LeanAlpha(0, slicedProcess / 4).setEaseOutExpo().setDelay(slicedProcess / 1.5f);

        // Background Panel
        backgroundPanel.fillOrigin = 1;
        LeanTween.value(backgroundPanel.gameObject, (float i) => backgroundPanel.fillAmount = i, 1.25f, 0, slicedProcess / 2).setDelay(slicedProcess / 2).setEaseInOutQuart();
    }

    public void AssignTips()
    {
        float tipsTransition = processDuration / 4;
        float slicedTransition = tipsTransition / 2;

        tipsText.gameObject.LeanCancel();

        tipsCanvas.alpha = 0;
        tipsCanvas.LeanAlpha(1, slicedTransition).setDelay(loadingOffset - slicedTransition).setEaseOutExpo();

        string tips = "DELIS";
        if (gameTips.Length > 0) tips = gameTips[UnityEngine.Random.Range(0, gameTips.Length - 1)];

        tipsText.text = $"\" {tips} \"";

        tipsCanvas.LeanAlpha(0, slicedTransition).setDelay(loadingOffset + slicedTransition).setEaseInExpo();
    }

    public static IEnumerator Process(Action action)
    {
        float timeScale = Time.timeScale;

        Time.timeScale = 0;

        bool complete = false;
        Debug.Log(complete);

        while (!complete)
        {
            yield return new WaitForEndOfFrame();
            action?.Invoke();
            yield return new WaitForEndOfFrame();
            complete = true;
        }

        Time.timeScale = timeScale;
    }

}
