using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace LdJam44.UI
{
    public class GameOverUi : UIBehaviour
    {
        [Header("References")]
        public GameObject Panel;

        public GameObject[] ItemsToDeactivate;

        [Header("Variables")]
        public float TimeToWaitBeforeShowingUi = 3;
        
        public void Restart()
        {
            SceneManager.LoadScene(Scenes.MrUnicornator);
        }

        public void MenuMenu()
        {
            SceneManager.LoadScene(Scenes.MainMenu);
        }

        public void GameOver()
        {
            StartCoroutine(DoGameOver());
        }

        private IEnumerator DoGameOver()
        {
            yield return new WaitForSeconds(TimeToWaitBeforeShowingUi);
            
            foreach (var gO in ItemsToDeactivate)
            {
                gO.SetActive(false);
            }
            
            Panel.SetActive(true);  
        }
    }
}
