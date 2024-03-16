using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MiniGame"); // Carrega a cena do minigame
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("TutorialScene"); // Carrega a cena do tutorial
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Encerra a execução no editor do Unity
        #else
            Application.Quit(); // Sai do aplicativo quando executado como build
        #endif
    }
}
