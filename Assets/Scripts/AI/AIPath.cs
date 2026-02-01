using TMPro;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.GridLayoutGroup;

[RequireComponent(typeof(NavMeshAgent))]
public class AIPath : MonoBehaviour
{
    public Vector3[] Points => agent.path.corners;
    public float Distance => ComputeDistance();

    public bool draw;

    NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    float ComputeDistance()
    {
        float distance = 0f;
        for (int i = 1; i < Points.Length; i++)
        {
            distance += Vector3.Distance(Points[i - 1], Points[i]);
        }
        return distance;
    }

    private void Update()
    {
        if (draw)
        {
            for (int i = 1; i < Points.Length; i++)
            {
                Debug.DrawLine(Points[i - 1], Points[i], Color.green);
            }
        }
    }
}
