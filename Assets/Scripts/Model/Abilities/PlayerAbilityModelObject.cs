using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Ability", menuName = "Player/Ability/Create", order = 0)]
public class PlayerAbilityModelObject : ScriptableObject
{
    [field: Header("Info")]
    [field: SerializeField] public AbilityName AbilityName { get; private set; }

    [field: Header("Component Refs")]
    
    [field: SerializeField] public Sprite AbilityIcon;
    [field: SerializeField] public AssetReferenceAbility AbilityObject { get; private set; }

    [field: Header("Attributes")]

    [field: SerializeField] public float PowerCoolDownDuration;
    [field: SerializeField] public float PowerAmmoSize = 1;
}