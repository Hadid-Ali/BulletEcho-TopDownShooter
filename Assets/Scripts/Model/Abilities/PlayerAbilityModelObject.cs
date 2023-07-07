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
    [field: SerializeField] public AssetReferenceGameObject AbilityObject { get; private set; }

    [field: Header("Attributes")]

    [field: SerializeField] public float PowerCoolDownDuration;
    [field: SerializeField] public float PowerAmmoSize = 1;
    
}

[Serializable]
public class AssetReferenceAbility : AssetReferenceT<AbilityObject>
{
    /// <summary>
    /// Constructs a new reference to a GameObject.
    /// </summary>
    /// <param name="guid">The object guid.</param>
    public AssetReferenceAbility(string guid) : base(guid)
    {
    }
}
