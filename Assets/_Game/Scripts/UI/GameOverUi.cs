using System.Collections;
using LdJam44.Variables;
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

        public IntVariable PlayerLife;
        public IntVariable Points;
        public IntVariable DriverLane;
        public FloatVariable DriverXPosition;
        
        public void Restart()
        {
            ResetVariables();
            SceneManager.LoadScene(Scenes.MrUnicornator);
        }

        public void MenuMenu()
        {
            ResetVariables();
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

        private void ResetVariables()
        {
            PlayerLife.Value = PlayerLife.InitialValue;
            Points.Value = Points.InitialValue;
            DriverLane.Value = DriverLane.InitialValue;
            DriverXPosition.Value = DriverXPosition.InitialValue;
        }
    }
}
