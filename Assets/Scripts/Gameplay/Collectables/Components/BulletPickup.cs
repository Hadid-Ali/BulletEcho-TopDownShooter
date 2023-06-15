using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : Collectable
{
    [SerializeField] private int m_BulletsCount = 10;
    public override void Collect()
    {
        gameObject.SetActive(false);

        GameEvents.GameplayEvents.CollectBullet.Raise(m_BulletsCount);
    }
}
