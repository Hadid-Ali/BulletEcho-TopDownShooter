using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHealthController : HealthController
{
    [SerializeField] private HealthBar m_HealthBar;

    protected override void OnEnable()
    {
        base.OnEnable();
        m_HealthUpdate += m_HealthBar.UpdateHealthBar;
    }

    protected override void OnDisable()
    {
        m_HealthUpdate -= m_HealthBar.UpdateHealthBar;
        base.OnDisable();
    }

    protected override void OnHealthDiminish()
    {
        base.OnHealthDiminish();
        m_HealthBar.Hide();
    }
}
