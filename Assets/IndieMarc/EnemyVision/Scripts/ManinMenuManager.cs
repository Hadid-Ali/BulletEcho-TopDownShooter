using UnityEngine;
using UnityEngine.SceneManagement;

public class ManinMenuManager : MonoBehaviour
{
    public void LoadEnvironment(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
