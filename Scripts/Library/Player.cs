using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Player
{
    public string name;
    public Sprite avatar;
    public int jPoint = 6000;
    public bool autohit = false;
    public bool tutorial = true;
    public bool creator = false;

    public void Save()
    {
        this.SaveJson<Player>(Application.persistentDataPath, "/User", "Player");
    }

    public static void Load(Player player)
    {
        if (!SLS.LoadJson<Player>(player, Application.persistentDataPath, "/User", "Player")) player.Save();
    }



}