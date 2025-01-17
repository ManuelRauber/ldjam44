using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace LdJam44.UI
{
    public class MainMenu : UIBehaviour
    {
        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void StartGame()
        {
            SceneManager.LoadScene(Scenes.MrUnicornator);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Debug.developerConsoleVisible = !Debug.developerConsoleVisible;
            }
        }
    }
}
