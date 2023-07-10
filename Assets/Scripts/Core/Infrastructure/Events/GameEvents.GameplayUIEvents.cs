
using System;

public static partial class GameEvents
{
    public static class GameplayUIEvents
    {
        public static GameEvent<string> InputPromptEvent = new();
        public static GameEvent HidePromptEvent = new();
        
        public static GameEvent<bool> InventoryStatusChangedEvent = new();

        public static GameEvent<float, float> UpdateShieldBar = new();
        public static GameEvent<PowerButtonDataObject[]> PowersInitialized = new();
    }
}
