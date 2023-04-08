using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : HealthController
{
    public void RegisterHealthBarEvent(Action<float> onHealthUpdateMethod)
    {
        m_HealthUpdate += onHealthUpdateMethod;
    }
}
