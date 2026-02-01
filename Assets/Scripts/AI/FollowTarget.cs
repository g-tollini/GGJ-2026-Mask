using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float MinDistance = 1.5f;
    public float MaxDistance = 10f;
    NavMeshAgent agent;

    void Start()
    { 
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = MinDistance;

        if (target == null)
            target = FindFirstObjectByType<IsoTPSController>().transform;
    }

    void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);
    }
}
