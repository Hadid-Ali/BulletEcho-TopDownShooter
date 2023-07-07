using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityObject : MonoBehaviour
{
    public abstract void OnApply();

    public virtual void OnSpawn()
    {
        
    }
}
