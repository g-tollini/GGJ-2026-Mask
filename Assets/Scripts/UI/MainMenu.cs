using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "Game";

    public void Play()
    {
        SceneManager.LoadScene(gameSceneName);
        enabled = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
