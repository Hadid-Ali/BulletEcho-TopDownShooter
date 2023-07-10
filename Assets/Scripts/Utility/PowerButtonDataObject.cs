using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PowerButtonDataObject
{
    public Action<UIContext> ButtonAction;
    public UIContext UIContext;
}
