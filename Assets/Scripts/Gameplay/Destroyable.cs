using UnityEngine;

public class Destroyable : MonoBehaviour
{
    public float Price = 1f;
    public float WobbleIntensity = 0.02f;
    public float WobbleRate = 20f;
    public float finalShrinkScale = 0.3f;
    public bool finallyDestroyed = false;

    [HideInInspector] bool destroyed = false;

    [HideInInspector] float timeDelay = 0f;

    [HideInInspector] float destroyedTime = 0;

    private void Update()
    {
        if (destroyed)
        {
            if (Time.time - destroyedTime >= timeDelay)
            {
                if (finallyDestroyed)
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
        initScale = transform.localScale;
    }

    Vector3 initScale;

    void Wobble()
    {
        transform.localScale = Vector3.Scale(initScale, WobbledVector(Time.time - destroyedTime));
    }

    Vector3 WobbledVector(float deltaTime) => Vector3.one * (Mathf.Lerp(1f, finalShrinkScale, (Time.time - destroyedTime) / timeDelay) + WobbleIntensity * Mathf.Sin(deltaTime * WobbleRate));
}
