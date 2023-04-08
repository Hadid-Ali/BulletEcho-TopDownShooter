using System;
using UnityEngine;

namespace Gameplay.Projectile
{
    public class Rocket : SimpleProjectile
    {
        [SerializeField] private GameObject m_Expolosion;

        private void OnCollisionEnter(Collision collision)
        {
            Instantiate(m_Expolosion, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
