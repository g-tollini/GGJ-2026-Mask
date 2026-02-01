using Unity.Mathematics;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjectives : MonoBehaviour
{
    public float DamageCount = 0f;

    public float moneyLossAmount = 10f;

    public Transform[] randomRooms;
    public Transform ventilator;

    public void Destroyed(Destroyable destroyable)
    {
        DamageCount += destroyable.Price;
        destroyable.Destroy();
    }

    public void TriggerWin(Collider col)
    {
        if (col.GetComponent<IsoTPSController>() != null)
        {
            Win();
        }
    }

    public void TriggerLose(Collider col)
    {
        if (col.GetComponent<IsoTPSController>() != null)
        {
            Lose();
        }
    }

    public void Win()
    {
        SceneManager.LoadScene("CreditsScene", LoadSceneMode.Single);
    }

    public void Lose()
    {
        SceneManager.LoadScene(MainMenu.gameOverSceneName, LoadSceneMode.Single);
    }

    public void LoseMoney()
    {
        DamageCount -= moneyLossAmount;
        DamageCount = math.max(DamageCount, 0);
    }
}
