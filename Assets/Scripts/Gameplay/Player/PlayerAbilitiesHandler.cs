using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitiesHandler : MonoBehaviour
{
    [SerializeField] private List<PlayerAbilityModelObject> m_Abilities = new();

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        PowerButtonDataObject[] dataObjects = new PowerButtonDataObject[m_Abilities.Count];

        for (int i = 0; i < m_Abilities.Count; i++)
        {
            PlayerAbilityModelObject abilityModelObject = m_Abilities[i];
            
            dataObjects[i] = new PowerButtonDataObject()
            {
                ButtonAction = ActivatePower,
                UIContext = new UIContext()
                {
                    ID = i,
                    Sprite = abilityModelObject.AbilityIcon,
                    FillerAmount = abilityModelObject.PowerCoolDownDuration
                }
            };
        }
        GameEvents.GameplayUIEvents.PowersInitialized.Raise(dataObjects);
    }

    private void ActivatePower(UIContext uiContext)
    {
        //    m_Abilities[uiContext.ID].AbilityIcon
        Debug.LogError(m_Abilities[uiContext.ID].AbilityName);
    }

    private void OnPowerSpawned()
    {
        
    }
}
