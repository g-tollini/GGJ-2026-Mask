using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIPath))]
public class AIPathRenderer : MonoBehaviour
{
    public GameObject renderer;
    public Vector3 fromOffset;
    public Vector3 toOffset;
    List<GameObject> pipes = new List<GameObject>();

    AIPath path;

    void Start()
    {
        path = GetComponent<AIPath>();
    }

    private void Update()
    {
        while (pipes.Count < path.Points.Length)
        {
            pipes.Add(GameObject.Instantiate(renderer));
            pipes[pipes.Count - 1].transform.localScale = new Vector3(0.1f, 0.5f, 0.1f);
            var collider = pipes[pipes.Count - 1].GetComponent<Collider>();
            if (collider != null)
                collider.enabled = false;
        }

        for (int i = 0; i < pipes.Count; i++)
        {
            if (i < path.Points.Length - 1)
            {
                pipes[i].SetActive(true);
                Vector3 start = path.Points[i];
                if (i == 0)
                    start += fromOffset;
                Vector3 end = path.Points[i + 1];
                if (i + 1 == path.Points.Length - 1)
                    end += toOffset;
                Vector3 midPoint = (start + end) / 2f;
                pipes[i].transform.position = midPoint;
                Vector3 direction = end - start;
                pipes[i].transform.up = direction.normalized;
                pipes[i].transform.localScale = new Vector3(0.1f, direction.magnitude / 2f, 0.1f);
            }
            else
            {
                pipes[i].SetActive(false);
            }
        }
    }
}
