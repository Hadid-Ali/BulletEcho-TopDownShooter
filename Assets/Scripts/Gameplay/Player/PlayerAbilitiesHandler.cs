using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerAbilitiesHandler : MonoBehaviour
{
    [SerializeField] private List<PlayerAbilityModelObject> m_Abilities = new();

    private Transform m_Transform;

    private void Awake()
    {
        m_Transform = transform;
    }

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
        Addressables.InstantiateAsync(m_Abilities[uiContext.ID].AbilityObject).Completed += OnPowerSpawned;
    }

    private void OnPowerSpawned(AsyncOperationHandle<GameObject> obj)
    {
        obj.Result.GetComponent<AbilityObject>().Execute(m_Transform);
    }
}
