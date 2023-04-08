using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Weapons
{
    public class WeaponVFXHandler : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> m_MuzzleEffects;
        [SerializeField] private ParticleSystem m_BulletImpact;

        public void ShowBulletImpact(Vector3 impactPosition)
        {
            ParticleSystem impactParticle =  Instantiate(m_BulletImpact, impactPosition, Quaternion.identity);
            impactParticle.Play(true);

            Destroy(impactParticle, 1.2f);
        }

        public void ShowMuzzleEffects()
        {
           m_MuzzleEffects.ForEach(effect => effect.Play(true));
        }
    }
}
