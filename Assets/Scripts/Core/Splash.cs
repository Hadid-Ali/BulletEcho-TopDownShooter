using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    
    void Start()
    {
        SceneManager.LoadScene("Gameplay Scene");
    }
}
