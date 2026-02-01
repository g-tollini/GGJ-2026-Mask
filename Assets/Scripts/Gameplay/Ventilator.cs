using UnityEngine;

public class Ventilator : MonoBehaviour
{
    public float gameOverDistance = 2f;

    GameObjectives objectives;
    public void TriggerEnter(Collider col)
    {
        if (col.GetComponent<Enemy>() != null)
            objectives.Lose();
    }

    public void Enter(Collision col)
    {
        if (col.gameObject.GetComponent<Enemy>() != null)
            objectives.Lose();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
            objectives.Lose();
    }
}
