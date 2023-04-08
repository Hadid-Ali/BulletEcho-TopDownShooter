using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneScopeBehaviorSingleton<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }

    public void Awake()
    {
        if (Instance is not null)
        {
            Destroy(this);
        }
        Debug.LogError("Singleton");
        Instance = this as T;
    }
}
