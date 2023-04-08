using System;
using UnityEngine;

namespace Gameplay.Weapons
{
    [RequireComponent(typeof(AudioSource))]
    public class WeaponSFXHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource m_AudioSource;

        [SerializeField] private AudioClip m_ShotAudioClip;
        [SerializeField] private AudioClip m_DryShotAudioClip;

        public void ShootSound()
        {
            m_AudioSource.PlayOneShot(m_ShotAudioClip);
        }

        public void DryShootSound()
        {
            m_AudioSource.PlayOneShot(m_DryShotAudioClip);
        }
    }
}
