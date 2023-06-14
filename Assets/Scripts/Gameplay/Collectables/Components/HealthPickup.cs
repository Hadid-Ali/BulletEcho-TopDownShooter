using UnityEngine;

public class HealthPickup : Collectable
{
    public override void Collect()
    {
        Debug.LogError("Health Pickup");
        gameObject.SetActive(false);

        GameEvents.GameplayEvents.RestoreHealth.Raise(50);
    }

    
}