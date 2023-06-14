using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image m_HealthBarImage;
    [SerializeField] private Image m_ShieldBarImage;
    [SerializeField] private FloatPairEvent m_HealthUpdateEvent;
    
    private void OnEnable()
    {
        m_HealthUpdateEvent.Register(UpdateHealthBar);
        GameEvents.GameplayUIEvents.UpdateShieldBar.Register(UpdateShieldBar);
    }

    private void OnDisable()
    {
        m_HealthUpdateEvent.Unregister(UpdateHealthBar);
        GameEvents.GameplayUIEvents.UpdateShieldBar.UnRegister(UpdateShieldBar);
    }



    private void UpdateHealthBar(FloatPair floatPair)
    {
        UpdateHealthBar(floatPair.Item1 / floatPair.Item2);
    }
    
    protected virtual void UpdateHealthBar(float value)
    {
        Debug.Log($"Value {value}");
        m_HealthBarImage.fillAmount = value;
    }

    void UpdateShieldBar(float min, float max)
    {
        SetShieldBar(min / max);
    }

    void SetShieldBar(float value)
    {
        if(m_ShieldBarImage != null)
        {
            m_ShieldBarImage.fillAmount = value;
        }

    }

    protected virtual void Show()
    {
        gameObject.SetActive(true);
    }

    protected virtual  void Hide()
    {
        gameObject.SetActive(false);
    }
}
