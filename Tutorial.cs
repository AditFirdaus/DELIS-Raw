using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public static Transform noteArea;
    public Transform _noteArea;
    public AudioClip backgroundMusic;
    public AudioSource audioSource;
    public TutorialPhase[] tutorialPhases;

    public Button Skip;

    public float beginDelay;

    private void Awake()
    {
        noteArea = _noteArea;
    }

    private void Start()
    {
        Player.Load(Game.player);

        if (!Game.player.tutorial) Skip.gameObject.SetActive(true);
        else Skip.gameObject.SetActive(false);

        StartCoroutine(StartPhases());
    }

    public static void PlayTutorial()
    {
        SceneManager.LoadScene("Tutorial");
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
        Skip.interactable = false;
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
