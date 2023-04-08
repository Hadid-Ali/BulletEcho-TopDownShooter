using Gameplay.Projectile;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class SimpleRocketLauncher : BaseWeapon
    {
        [SerializeField] private Transform m_LaunchPoint;
        [SerializeField] private SimpleProjectile m_Projectile;

        protected override void FireInternal()
        {
            base.FireInternal();
            
            SimpleProjectile projectile = Instantiate(m_Projectile, m_LaunchPoint.position,m_LaunchPoint.transform.rotation);
            projectile.Launch(m_CurrentAimObject?.Target.transform.position ?? Vector3.zero,m_WeaponDamage);
        }
    }
}
