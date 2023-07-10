using System;
using UnityEngine;

public class PowerButtonsContainer : MonoBehaviour
{
    [SerializeField] private ButtonWithFiller m_ButtonPrefab;
    [SerializeField] private Transform m_ButtonsContainer;

    private void OnEnable()
    {
        GameEvents.GameplayUIEvents.PowersInitialized.Register(CreateButtons);
    }

    private void OnDisable()
    {
        GameEvents.GameplayUIEvents.PowersInitialized.Unregister(CreateButtons);
    }

    public void CreateButtons(PowerButtonDataObject[] powerDataObjects)
    {
        for (int i = 0; i < powerDataObjects.Length; i++)
        {
            PowerButtonDataObject powerButtonDataObject = powerDataObjects[i];
            ButtonWithFiller fillerButton = Instantiate(m_ButtonPrefab, m_ButtonsContainer);
            
            fillerButton.SetContext(powerButtonDataObject.UIContext);
            fillerButton.SubscribeEvent(powerButtonDataObject.ButtonAction);
        }
    }
}
