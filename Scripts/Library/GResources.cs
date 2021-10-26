using UnityEngine;

[System.Serializable]
public class GResources
{
    public SoundEffects soundEffects;
    public Objects objects;
    public Data data;

    // Classes
    [System.Serializable]
    public class SoundEffects
    {
        // UI
        public AudioClip UI_Button_Click_1;
        public AudioClip UI_Button_Click_2;
    }

    [System.Serializable]
    public class Objects
    {
        public GameObject Screen_Loading;
    }

    [System.Serializable]
    public class Data
    {
        public Gameplay gameplay;

        // Classes
        [System.Serializable]
        public class Gameplay
        {
            public LevelPack Tutorial_LevelPack;
            public LevelPack Tutorial_LevelData;
        }
    }
}