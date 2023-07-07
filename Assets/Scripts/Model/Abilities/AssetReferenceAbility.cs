using System;
using UnityEngine.AddressableAssets;

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

