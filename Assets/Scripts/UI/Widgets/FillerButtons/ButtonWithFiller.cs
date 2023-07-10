using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWithFiller : ButtonWithContext
{
    [SerializeField] private FillerImage m_FillerImage;
    
    public override void SetContext(UIContext context)
    {
        base.SetContext(context);
        
        m_FillerImage.Initialize(EnableButton, DisableButton, context.FillerAmount);
        SubscribeClickInternal(() => m_FillerImage.Consume(1));
    }

    private void EnableButton()
    {
        m_ButtonComponent.interactable = true;
    }

    private void DisableButton()
    {
        m_ButtonComponent.interactable = false;
    }
}
