using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectionHandler : PlayerRangeHandler
{
    private Collectable m_Collectable;
    
    protected override void OnObjectEnterRange(GameObject rangeObject)
    {
        if (!rangeObject.TryGetComponent(out m_Collectable))
            return;
        
        GameEvents.GameplayEvents.EnterObjectRange.Raise(CollectCurrentObject);
    }

    protected override void OnObjectLeaveRange(GameObject rangeObject)
    {
        if (!rangeObject.TryGetComponent(out m_Collectable))
            return;
        
        GameEvents.GameplayEvents.ExitObjectRange.Raise();
    }

    private void CollectCurrentObject()
    {
        m_Collectable.Collect();
    }
}
