using Unity;
using UnityEngine;

class Spawnpoint : MonoBehaviour
{
    public GameObject[] agentPrefabs;
    public GameObjectives objectives;

    public float spawnDelay = 10;
    float spawnTime;

    private void Start()
    {
       spawnTime = spawnDelay;
    }

    private void Update()
    {
        //spawnTime -= Time.deltaTime;
        //if (spawnTime <= 0)
        //{
        //    spawnTime = spawnDelay;
        //    Spawn();
        //}
    }

    void Spawn()
    {
        var agent = agentPrefabs[Random.Range(0, agentPrefabs.Length)];
        var instance = Instantiate(agent, transform.position, transform.rotation);
        var followTarget = instance.GetComponent<FollowTarget>();
        followTarget.objectives = objectives;
        followTarget.ventilator = objectives.ventilator;

    }
}
