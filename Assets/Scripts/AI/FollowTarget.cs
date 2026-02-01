using UnityEngine;
using UnityEngine.AI;

public enum AgentBehavior
{
    AttackPlayer,
    Flee,
    StopVentilator,
}

[RequireComponent(typeof(NavMeshAgent))]
public class FollowTarget : MonoBehaviour
{
    Transform target;
    public Transform ventilator;
    public Transform[] randomRooms;
    public float MinDistance = 1.5f;
    public float MaxDistance = 10f;
    public AgentBehavior behavior;
    NavMeshAgent agent;

    public float stoppingVentilatorDelay = 10;
    private float stoppingVentilatorTime;

    void Start()
    { 
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = MinDistance;
        stoppingVentilatorTime = stoppingVentilatorDelay;

        switch (behavior)
        {

            case AgentBehavior.AttackPlayer:
                target = FindFirstObjectByType<IsoTPSController>().transform;
                break;

            case AgentBehavior.StopVentilator:
                target = ventilator;
                break;

            case AgentBehavior.Flee:
                target = randomRooms[Random.Range(0, randomRooms.Length)];
                break;
        }
    }

    void Update()
    {
        agent.SetDestination(target.position);
        gameObject.transform.LookAt(target.position);

        switch (behavior)
        {

            case AgentBehavior.AttackPlayer:
                if (agent.remainingDistance == MinDistance)
                {

                }
                break;

            case AgentBehavior.StopVentilator:
                target = ventilator;
                if (agent.remainingDistance <= MinDistance + 1.5f)
                {
                    stoppingVentilatorTime -= Time.deltaTime;
                    Debug.Log($"Killing {stoppingVentilatorTime} sec");
                } else
                {
                    //stoppingVentilatorTime = stoppingVentilatorDelay;
                }
                break;

            case AgentBehavior.Flee:
                target = randomRooms[Random.Range(0, randomRooms.Length)];

                break;
        }

        if (stoppingVentilatorTime <= 0)
        {
            Debug.Log("YOU DIED");
        }
    }
}
