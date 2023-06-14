using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectionHandler : PlayerRangeHandler
{
    private Collectable m_Collectable;
    
    protected override void OnObjectEnterRange(GameObject rangeObject)
    {
        if (!rangeObject.TryGetComponent(out Collectable collectable))
            return;

        m_Collectable = collectable;
        GameEvents.GameplayEvents.WaitForAction.Raise(CollectCurrentObject);
    }

    protected override void OnObjectLeaveRange(GameObject rangeObject)
    {
        if (!rangeObject.TryGetComponent(out Collectable collectable))
            return;
        
        GameEvents.GameplayEvents.CancelWaitForAction.Raise();
    }

    private void CollectCurrentObject()
    {
        m_Collectable.Collect();
        m_Collectable = null;
    }
}