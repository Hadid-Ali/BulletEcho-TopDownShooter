using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image m_HealthBarImage;
    [SerializeField] private FloatPairEvent m_HealthUpdateEvent;
    
    private void OnEnable()
    {
        m_HealthUpdateEvent.Register(UpdateHealthBar);
    }

    private void OnDisable()
    {
        m_HealthUpdateEvent.Unregister(UpdateHealthBar);
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

    protected virtual void Show()
    {
        gameObject.SetActive(true);
    }

    protected virtual  void Hide()
    {
        gameObject.SetActive(false);
    }
}
