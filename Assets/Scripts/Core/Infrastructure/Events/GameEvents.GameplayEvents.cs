using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class GameEvents
{
    public static class GameplayEvents
    {
        public static GameEvent<Action> EnterObjectRange = new();
        public static GameEvent ExitObjectRange = new();

    }
}
