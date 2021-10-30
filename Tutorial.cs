using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public static Transform noteArea;
    public Transform _noteArea;
    public AudioClip backgroundMusic;
    public AudioSource audioSource;
    public TutorialPhase[] tutorialPhases;

    public float beginDelay;

    private void Awake()
    {
        noteArea = _noteArea;
    }

    private void Start()
    {
        StartCoroutine(StartPhases());
    }

    public static void PlayTutorial()
    {
        LoadingScreen.Load(() => SceneManager.LoadScene("Tutorial"));
    }

    public IEnumerator StartPhases()
    {
        yield return new WaitForSeconds(beginDelay);

        StartTutorial();

        foreach (TutorialPhase phase in tutorialPhases)
        {
            yield return StartCoroutine(phase.Activate());
        }
        EndTutorial();
    }

    public void StartTutorial()
    {
        audioSource.clip = backgroundMusic;
        audioSource.Play();
        audioSource.LeanVolume(0, 0.5f, 3);
    }

    public void EndTutorial()
    {
        audioSource.LeanVolume(0.5f, 0, 3).setOnComplete(
            () => Menu()
        );
        Game.player.tutorial = false;
        Game.player.Save();
    }

    public void Menu()
    {
        audioSource.Stop();
        LoadingScreen.Load(() => MainMenu.Menu());
    }

}
