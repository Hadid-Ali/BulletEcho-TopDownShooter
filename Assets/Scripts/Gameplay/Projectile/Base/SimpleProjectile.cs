using System;
using UnityEngine;

namespace Gameplay.Projectile
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class SimpleProjectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody m_Rigidbody;
        [SerializeField] private float m_ProjectileSpeed;

        protected float m_Damage;

        private void OnValidate()
        {
            m_Rigidbody ??= GetComponent<Rigidbody>();
        }

        public void Launch(Vector3 targetPosition = default,float damage = 5)
        {
            Vector3 travelDirection = transform.forward;
            m_Damage = damage;
            if (targetPosition != default)
            {
                travelDirection = targetPosition - transform.position;
                transform.LookAt(targetPosition);
            }
            m_Rigidbody.AddForce(travelDirection.normalized*m_ProjectileSpeed);
        }
    }
}
