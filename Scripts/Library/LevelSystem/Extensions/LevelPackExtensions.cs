using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;

public static class LevelPackExtensions
{

    public static void SaveData(this LevelPack levelPack)
    {
        SLS.SaveJson<LevelPack.Data>(
            levelPack.data,
            Application.persistentDataPath,
            $"/SaveData/LevelPack",
            $"{levelPack.packName}"
        );
    }

    public static void LoadData(this LevelPack levelPack)
    {
        bool loaded = SLS.LoadJson<LevelPack.Data>(
            levelPack.data,
            Application.persistentDataPath,
            $"/SaveData/LevelPack",
            $"{levelPack.packName}"
        );



        if (!loaded)
        {
            levelPack.data = new LevelPack.Data();
            levelPack.SaveData();
            levelPack.LoadData();
        }
    }

    public static void Buy(this LevelPack levelPack)
    {
        if (Game.player.jPoint >= levelPack.cost && !levelPack.data.purchased)
        {
            Game.player.jPoint -= levelPack.cost;
            levelPack.data.purchased = true;
            levelPack.SaveData();

            Game.player.Save();
        }
    }

    public static void LoadLevelData(this LevelPack levelPack)
    {
        levelPack.levelDatas = levelPack.GetLevelDatas();

        foreach (LevelData levelData in levelPack.levelDatas)
        {
            levelData.packName = levelPack.packName;
        }
    }

    public static void LoadMusics(this LevelPack levelPack)
    {
        foreach (LevelData levelData in levelPack.levelDatas)
        {
            levelData.LoadMusic();
        }
    }

    public static void UnLoadMusic(this LevelPack levelPack)
    {
        foreach (LevelData levelData in levelPack.levelDatas)
        {
            levelData.UnLoadMusic();
        }
    }

    public static LevelData[] GetLevelDatas(this LevelPack levelPack)
    {
        string folderPath = $"Game/Level/LevelData/{levelPack.packName}";

        SLS.CreateDirectory(
            Application.dataPath +
            "/Resources/" +
            folderPath,
            true
            );

        LevelData[] levelDatas = Resources.LoadAll<LevelData>(folderPath); Debug.Log($"Asset Loaded : {levelDatas}");

        return levelDatas;
    }

    public static LevelPack[] LoadLevelPacks()
    {
        string folderPath = $"Game/LevelPack";

        SLS.CreateDirectory(
            Application.dataPath +
            "/Resources/" +
            folderPath,
            true
            );

        LevelPack[] levelPacks = Resources.LoadAll<LevelPack>(folderPath); Debug.Log($"Asset Loaded : {levelPacks}");

        foreach (LevelPack levelPack in levelPacks)
        {
            levelPack.LoadLevelData();
        }

        return levelPacks;
    }

    public static string[] GetLevelDataNames(this LevelPack levelPack)
    {
        LevelData[] levelDatas = levelPack.GetLevelDatas();

        string[] names = new string[levelDatas.Length];

        for (int i = 0; i < levelDatas.Length; i++)
        {
            names[i] = levelDatas[i].levelName;
        }

        return names;
    }

}
