using LdJam44.EventSystem;
using LdJam44.Variables;
using UnityEngine;

namespace LdJam44.Managers
{
    public class GameOverManager : MonoBehaviour
    {
        [Header("References")]
        public IntVariable PlayerLife;

        public GameEvent GameOverEvent;

        private void Update()
        {
            if (PlayerLife > 0)
            {
                return;
            } 
            
            GameOver();
        }

        private void GameOver()
        {
            Debug.Log("Game over!");
            
            GameOverEvent.Raise();
            Destroy(this);
        }
    }
}
