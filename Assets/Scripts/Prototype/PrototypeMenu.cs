using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrototypeMenu : MonoBehaviour
{
   [SerializeField] private Image[] m_ButtonImages;
   private static int m_CurrentCharacter = 0;
   private void Start()
   {
       ChangeCharacter(m_CurrentCharacter);
   }

   public void ChangeCharacter(int index)
   {
       for (int i = 0; i < m_ButtonImages.Length; i++)
       {
           m_ButtonImages[i].color = i == index ? Color.green : Color.white;
       }
       GameManager.Instance.GameplayManager.SpawnPlayer(index);
   }

   public void ChangeCharacterNewScene(int index)
   {
       m_CurrentCharacter = index;
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
   }
}
