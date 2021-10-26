using UnityEngine;

public static class LevelResultExtensions
{
    public static void SaveResult(this LevelData levelData)
    {
        SLS.SaveJson<LevelResult>(
            levelData.levelResult,
            Application.persistentDataPath,
            $"/SaveData/LevelResult/{levelData.packName}",
            $"{levelData.levelName}"
        );
    }

    public static void LoadResult(this LevelData levelData)
    {
        bool loaded = SLS.LoadJson<LevelResult>(
            levelData.levelResult,
            Application.persistentDataPath,
            $"/SaveData/LevelResult/{levelData.packName}",
            $"{levelData.levelName}"
        );

        if (!loaded)
        {
            levelData.SaveResult();
            levelData.LoadResult();
        }
    }
}