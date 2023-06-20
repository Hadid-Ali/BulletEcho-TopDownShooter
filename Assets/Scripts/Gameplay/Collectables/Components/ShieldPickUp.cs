using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickUp : Collectable
{
    public override void Collect()
    {
        base.Collect();
        GameEvents.GameplayEvents.GainShield.Raise(100);
    }

}
