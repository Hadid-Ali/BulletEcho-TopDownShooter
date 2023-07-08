using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWithFiller : MonoBehaviour
{
    [SerializeField] private Button m_ButtonComponent;
    [SerializeField] private FillerImage m_FillerImage;

    private void Start()
    {
        
    }

    public void Initialize(Action onButtonPress,float fillerDuration)
    {
        m_ButtonComponent.onClick.AddListener(() =>
        {
            onButtonPress();
        });
        m_FillerImage.Initialize(EnableButton, DisableButton, fillerDuration);
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
