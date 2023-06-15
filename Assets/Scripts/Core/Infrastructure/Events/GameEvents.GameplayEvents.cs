using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class GameEvents
{
    public static class GameplayEvents
    {
        public static GameEvent<Action> WaitForAction = new();
        public static GameEvent CancelWaitForAction = new();

        public static GameEvent<float> RestoreHealth = new();
        public static GameEvent<float> GainShield = new();
        public static GameEvent<int> CollectBullet = new();

    }
}
