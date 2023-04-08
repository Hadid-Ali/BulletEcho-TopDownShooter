using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonobehaviourSingleton<GameManager>
{
    private GameplayManager m_GameplayManager;
    public GameplayManager GameplayManager => m_GameplayManager;

    private void Start()
    {
        Application.targetFrameRate = -1; 
    }

    public void RegisterGameplayManager(GameplayManager gameplayManager)
    {
        m_GameplayManager = gameplayManager;
    }

    public void UnRegisterGameplayManager(GameplayManager gameplayManager)
    {
        if(m_GameplayManager != gameplayManager)
            return;
        m_GameplayManager = null;
    }
}
