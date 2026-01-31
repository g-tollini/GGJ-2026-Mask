using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string gameSceneName = "Game";
    public string mainMenuSceneName = "MainMenu";

    public void Play()
    {
        SceneManager.LoadScene(gameSceneName);
        SceneManager.UnloadSceneAsync(mainMenuSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
