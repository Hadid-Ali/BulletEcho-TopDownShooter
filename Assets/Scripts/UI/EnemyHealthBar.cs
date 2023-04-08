using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : HealthBar
{
    [SerializeField] private GameObject m_Container;

    protected override void UpdateHealthBar(float value)
    {
        Show();
        
        if (IsInvoking(nameof(Hide)))
            CancelInvoke(nameof(Hide));
        
        Invoke(nameof(Hide), 2f);
        base.UpdateHealthBar(value);
    }
    
    protected override void Show()
    {
        m_Container.SetActive(true);
    }

    protected override void Hide()
    {
        m_Container.SetActive(false);
    }
}
