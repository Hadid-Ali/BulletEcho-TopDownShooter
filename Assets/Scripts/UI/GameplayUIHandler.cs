using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UI;
using UnityEngine.SceneManagement;

public class GameplayUIHandler : MonoBehaviour
{
    [SerializeField] private Crosshair m_Crosshair;
    [SerializeField] private HealthBar m_HealthBar;

    public Crosshair Crosshair => m_Crosshair;
    public HealthBar HealthBar => m_HealthBar;



    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
