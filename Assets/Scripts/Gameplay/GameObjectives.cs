using UnityEngine;

public class GameObjectives : MonoBehaviour
{
    public float DamageCount = 0f;

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
        Debug.Log("You Win!");
    }

    public void Lose()
    {
        Debug.Log("You Lose!");
    }
}
