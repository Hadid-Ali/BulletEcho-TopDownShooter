using UnityEngine;

namespace Gameplay.Weapons
{
    public class SimpleRifle : SimpleWeapon
    {
   
        protected override void FireInternal()
        {
            base.FireInternal();

            if (m_CurrentAimObject is not null)
            {
                m_WeaponVFXHandler.ShowBulletImpact(m_CurrentAimObject.Target.transform.position);
            }
        }
    }
}
