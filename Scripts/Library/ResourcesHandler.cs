using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesHandler : MonoBehaviour
{
    public static GResources gResources;
    public GResources _GResources;

    private void Awake()
    {
        gResources = _GResources;
    }
}
