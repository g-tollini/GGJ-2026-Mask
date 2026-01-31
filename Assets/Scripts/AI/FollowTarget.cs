using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FollowTarget : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;

    void Awake() => agent = GetComponent<NavMeshAgent>();

    void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);
    }
}
