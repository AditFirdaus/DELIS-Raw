using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{
    public UserStats userStats;
    public RectTransform screen;
    public CanvasGroup background;
    public AudioSource audioSource;
    public void Result()
    {

        background.gameObject.LeanCancel();
        background.LeanAlpha(1, 2).setIgnoreTimeScale(true);

        screen.LeanCancel();
        screen.LeanScale(Vector3.one * 4, 0).setIgnoreTimeScale(true);
        screen.LeanScale(Vector3.one, 1).setEaseOutExpo().setIgnoreTimeScale(true);

        ApplyResult();
        gameObject.SetActive(true);

        userStats.UpdateStats(Gameplay.levelData);
    }

    public void ApplyResult()
    {
        Gameplay.levelData.LoadResult();
        LevelResult levelResult = Gameplay.levelData.levelResult;
        if (GameplayData.highCombo > levelResult.highCombo) levelResult.highCombo = GameplayData.highCombo;
        if (GameplayData.highScore > levelResult.highScore) levelResult.highScore = GameplayData.highScore;
        Gameplay.levelData.SaveResult();

        ApplyReward(GameplayData.highScore);
    }

    public void ApplyReward(float score)
    {
        int reward = (int)((score / 1000000) * 5000);
        Debug.Log(reward);
        Game.player.jPoint += reward;
        Game.player.Save();
    }

    public void Restart()
    {
        audioSource.Stop();

        screen.LeanCancel();
        screen.LeanScale(Vector3.one * 4, 1).setEaseInExpo().setIgnoreTimeScale(true); ;


        background.gameObject.LeanCancel();
        background.LeanAlpha(0, 1).setIgnoreTimeScale(true).setOnComplete(
            () =>
            {
                Gameplay.Play(Gameplay.levelData);
            }
        );
    }

    public void Back()
    {
        audioSource.Stop();
        Gameplay.main.Back();
    }
}
