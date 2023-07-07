using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityObject : MonoBehaviour
{
    public abstract void Execute();

    private void OnDestroy()
    {
        Resources.UnloadUnusedAssets();
    }

    public virtual void OnSpawn()
    {
        
    }

    public virtual void DestroyAbilityObject()
    {
        
    }
}
