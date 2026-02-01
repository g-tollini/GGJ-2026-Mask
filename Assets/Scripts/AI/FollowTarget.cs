using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float MinDistance = 1.5f;
    public float MaxDistance = 10f;
    NavMeshAgent agent;

    void Awake()
    { 
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = MinDistance;
    }

    void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);
    }
}
