using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public float Price = 1f;
    public float WobbleIntensity = 0.02f;
    public float WobbleRate = 20f;

    [HideInInspector] bool destroyed = false;

    [HideInInspector] float timeDelay = 0f;

    [HideInInspector] float destroyedTime = 0;

    private void Update()
    {
        if (destroyed)
        {
            if (Time.time - destroyedTime >= timeDelay)
            {
                Destroy(gameObject);
            }
            else
            {
                Wobble();
            }
        }
    }

    public void Destroy(float delay = 1f)
    {
        if (destroyed) 
            return;

        destroyed = true;
        timeDelay = delay;
        destroyedTime = Time.time;
    }


    Renderer[] renderers;
    Vector3[] initScales;

    void Wobble()
    {
        if (renderers == null)
        {
            renderers = GetComponentsInChildren<Renderer>();
            initScales = new Vector3[renderers.Length];
            for (int i = 0; i < renderers.Length; i++)
                initScales[i] = renderers[i].transform.localScale;
        }

        for(int i = 0; i< renderers.Length; i++)
        {
            renderers[i].transform.localScale = Vector3.Scale(initScales[i], WobbledVector(Time.time - destroyedTime));
        }
    }

    Vector3 WobbledVector(float deltaTime) => Vector3.one * (1f + WobbleIntensity * Mathf.Sin(deltaTime * WobbleRate));
}
