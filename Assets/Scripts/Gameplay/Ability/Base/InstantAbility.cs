using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InstantAbility : AbilityObject
{
    protected override void OnSpawn()
    {
        base.OnSpawn();
        DestroyAbilityObject();
    }
}
