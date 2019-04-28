using UnityEditor;
#if !UNITY_EDITOR
using UnityEngine;
#endif
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
#if DEBUG
            SceneManager.LoadScene(Scenes.MrUnicornator);
#else
            SceneManager.LoadScene(Scenes.Game);
#endif
        }
    }
}
