using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

public class Game
{
    public static Player player = new Player();
}

public static class SLS
{
    public static T LoadResource<T>(string loadPath, string fileName) where T : UnityEngine.Object
    {
        string filePath = $"{loadPath}/{fileName}";

        T assetResource = Resources.Load<T>(filePath);

        return assetResource;
    }

    // Save object ke direktori dengan :
    // basePath : Sebagai start dalam direktori yang dituju
    // loadPath : Sebagai exstensi dari basePath untuk menuju ke direktori tertentu
    // filename : Nama file di direktori gabungan antara basePath dan loadPath
    public static bool SaveJson<T>(this T obj, string basePath, string savePath, string filename)
    {
        try
        {
            string folder = basePath + savePath;
            string file = folder + $"/{filename}.json";

            string data = JsonUtility.ToJson(obj, true);

            SLS.CreateDirectory(folder);
            File.WriteAllText(file, data);

            Debug.Log($"Save Berhasil Ke {file} dengan data {data}");
            return true;
        }
        catch (System.Exception)
        {
            Debug.Log("Save Gagal");
            return false;
        }
    }

    // Loading object dari direktori dengan :
    // basePath : Sebagai start dalam direktori yang dituju
    // loadPath : Sebagai exstensi dari basePath untuk menuju ke direktori tertentu
    // filename : Nama file di direktori gabungan antara basePath dan loadPath
    public static bool LoadJson<T>(T target, string basePath, string loadPath, string filename)
    {
        try
        {
            string folder = basePath + loadPath;
            string file = folder + $"/{filename}.json";

            SLS.CreateDirectory(folder);

            if (!File.Exists(file)) { Debug.Log($"Load File Gagal Dikarenakan File Yang Dituju Tidak Ada Di Direktori : {file}"); return false; } // Akan mereturn false jika file yang dituju tidak ada

            string data = File.ReadAllText(file);

            JsonUtility.FromJsonOverwrite(data, target);

            return true;
        }
        catch (System.Exception)
        {
            Debug.Log("Load Gagal");
            return false;
        }
    }

    public static bool CreateDirectory(string path, bool editorOnly = false)
    {
        if (editorOnly && !Application.isEditor) return true;

        bool state = false;
        try
        {
            string directoryPath = path;

            Directory.CreateDirectory(Path.GetDirectoryName(directoryPath));

            Debug.Log($"Directory Created on {directoryPath}");

            state = true;
        }
        catch (System.Exception)
        {
            Debug.Log("Create Directory Failed");
            state = false;
        }

        return state;
    }

}