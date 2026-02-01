using UnityEngine;

[RequireComponent(typeof(AIPath))]
public class Enemy : MonoBehaviour
{
    FollowTarget ft;
    AIPath path;

    void Start()
    {
        ft = GetComponent<FollowTarget>();
        path = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        var v = ft.target.GetComponent<Ventilator>();
        if (v != null && path.Distance > 0 && path.Distance < v.gameOverDistance)
            FindFirstObjectByType<GameObjectives>().Lose();
    }
}
