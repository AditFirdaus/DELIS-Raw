using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIndicator : MonoBehaviour
{
    float progress = 0;
    public AudioSource audioSource;
    public Image Top;
    public Image Bottom;


    private void Update()
    {
        if (audioSource.isPlaying)
        {
            progress = audioSource.time / audioSource.clip.length;
            Top.fillAmount = progress;
            Bottom.fillAmount = progress;
        }
    }

}
