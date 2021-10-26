using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JPanel : MonoBehaviour
{
    public static JPanel main;
    public TMP_Text RJPoint;

    private void Awake()
    {
        main = this;
    }

    public void UpdatePanel()
    {
        RJPoint.text = Game.player.jPoint.ToString();
    }

}
