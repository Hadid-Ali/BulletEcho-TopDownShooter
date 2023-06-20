using UnityEngine;

public class HealthPickup : Collectable
{
    [SerializeField] private float m_HealthAmountToReplinish = 50;
    
    public override void Collect()
    {
        base.Collect();
        GameEvents.GameplayEvents.RestoreHealth.Raise(m_HealthAmountToReplinish);
    }

    
}