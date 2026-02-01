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
    public Transform target;
    public Transform ventilator;
    public Transform[] randomRooms;
    public float MinDistance = 1.5f;
    public float minDistanceFromVentilator = 1.5f;
    public float minDistanceFromPlayer = 1.5f;
    public float MaxDistance = 10f;
    public AgentBehavior behavior;
    NavMeshAgent agent;
    public bool offensive = true;

    public GameObjectives objectives;

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

        if (offensive)
        {
            switch (behavior)
            {

                case AgentBehavior.AttackPlayer:
                    if (agent.remainingDistance <= MinDistance + minDistanceFromPlayer)
                    {
                        objectives.LoseMoney();
                    }
                    break;

                case AgentBehavior.StopVentilator:
                    target = ventilator;
                    if (agent.remainingDistance <= MinDistance + minDistanceFromVentilator)
                    {
                        stoppingVentilatorTime -= Time.deltaTime;
                    }
                    else
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
                objectives.Lose();
            }
        }
    }
}
