using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public enum MenuType
    {
        Main,
        GameOver,
    }

    public MenuType menu = MenuType.Main;

    public static string gameSceneName = "Game";
    public static string gameOverSceneName = "GameOver";

    public void Play()
    {
        SceneManager.LoadScene(gameSceneName);
        enabled = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(gameSceneName);
        enabled = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
