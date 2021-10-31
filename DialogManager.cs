using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager main;
    public GameObject Blocker;
    public ExitDialog exitDialog;
    public PurchaseDialog purchaseDialog;
    private void Awake()
    {
        main = this;
    }
}