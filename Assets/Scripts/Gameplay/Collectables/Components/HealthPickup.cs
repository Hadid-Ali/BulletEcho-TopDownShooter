using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Collectable
{
    public override void Collect()
    {
        Debug.LogError("Health Pickup");
        gameObject.SetActive(false);
    }
}