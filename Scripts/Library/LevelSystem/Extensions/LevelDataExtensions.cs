using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public static class LevelDataExtensions
{
    public static void LoadData(this LevelData levelData, bool music = false)
    {
        levelData.LoadResult();
        levelData.LoadNoteMap();
        levelData.LoadSprite();

        if (music) levelData.LoadMusic();
    }
    public static void LoadMusic(this LevelData levelData)
    {
        string filePath = $"Game/Level/Music/{levelData.packName}/{levelData.levelName}/{levelData.musicName}";

        SLS.CreateDirectory(
            Application.dataPath +
            "/Resources/" +
            Path.GetDirectoryName(filePath),
            true
            );

        levelData.ProcessLevelDataMusic();

        AudioClip audioClip = Resources.Load<AudioClip>(filePath); Debug.Log($"Asset Loaded : {audioClip}");

        levelData.musicClip = audioClip;

    }
    public static void UnLoadMusic(this LevelData levelData)
    {
        Resources.UnloadAsset(levelData.musicClip);
    }

    public static void LoadSprite(this LevelData levelData)
    {
        string filePath = $"Game/Level/Sprite/{levelData.packName}/{levelData.levelName}";

        SLS.CreateDirectory(
            Application.dataPath +
            "/Resources/" +
            Path.GetDirectoryName(filePath),
            true
            );


        Sprite sprite = Resources.Load<Sprite>(filePath); Debug.Log($"Asset Loaded : {sprite}");

        levelData.levelSprite = sprite;
    }

    public static void LoadNoteMap(this LevelData levelData)
    {
        Debug.Log("NoteMap");
        string filePath = $"Game/Level/NoteMap/{levelData.packName}/{levelData.levelName}";

        SLS.CreateDirectory(
            Application.dataPath +
            "/Resources/" +
            Path.GetDirectoryName(filePath),
            true
            );

        TextAsset textAsset = Resources.Load<TextAsset>(filePath); Debug.Log($"Asset Loaded : {textAsset}");

        if (textAsset is null) return;

        JsonUtility.FromJsonOverwrite(
        textAsset.text,
        levelData.noteMap
        ); Debug.Log("Loading Sucess");
    }

    public static void ProcessLevelDataMusic(this LevelData levelData)
    {
        string filePath = $"Game/Level/Music/{levelData.packName}/{levelData.levelName}/{levelData.musicName}";

        SLS.CreateDirectory(
            Application.dataPath +
            "/Resources/" +
            Path.GetDirectoryName(filePath),
            true
            );
        AudioClip[] audioClips = Resources.LoadAll<AudioClip>($"Game/Level/Music/{levelData.packName}/{levelData.levelName}");


        AudioClip audioClip = null;
        if (audioClips.Length > 0) audioClip = audioClips[0];

        /*
        if (audioClip)
        {
            Debug.Log("Processed");

            Debug.Log(
                UnityEditor.AssetDatabase.RenameAsset($"Assets/Resources/Game/Level/Music/{levelData.packName}/{levelData.levelName}/{audioClip.name}.mp3", levelData.musicName)
            );
        }
        */
    }

}
