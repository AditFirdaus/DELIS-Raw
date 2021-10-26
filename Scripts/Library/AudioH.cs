using UnityEngine;

public static class AudioH
{
    public static AudioSource PlaySafe(AudioClip audioClip)
    {
        if (audioClip)
        {
            AudioSource audioSource = LeanAudio.play(audioClip);
            return audioSource;
        }
        return null;
    }

    public static LTDescr LeanVolume(
        this AudioSource audioSource,
        float start = 0,
        float end = 0,
        float time = 1,
        LeanTweenType tweenType = LeanTweenType.linear
        )
    {
        LTDescr lTDescr = LeanTween.value(
            audioSource.gameObject,
            (float i) =>
            {
                audioSource.volume = i;
            },
            start,
            end,
            time
        ).setEase(tweenType);

        return lTDescr;
    }
}