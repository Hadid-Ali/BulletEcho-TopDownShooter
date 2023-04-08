using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private PlayerWeaponsController playerWeaponsController;
   [SerializeField] private WeaponAimingComponent m_AimingComponent;
   [SerializeField] private PlayerHealthController m_PlayerHealthController;
   
   public PlayerWeaponsController PlayerWeaponsController => playerWeaponsController;

   public void OnPlayerSpawn(Transform aimPoint,Action<float> healthBarEvent)
   {
      m_PlayerHealthController.RegisterHealthBarEvent(healthBarEvent);
      gameObject.SetActive(true);
   }

   public void Hide()
   {
      gameObject.SetActive(false);
   }
}
