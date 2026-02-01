using UnityEngine;
using UnityEngine.Events;

public class OnCollision : MonoBehaviour
{
    public LayerMask CollidesWith = ~0;

    public UnityEvent<Collider> TriggerEnter;
    public UnityEvent<Collision> Enter;

    void OnTriggerEnter(Collider c)
    {
        if ((1 << c.gameObject.layer & CollidesWith) == 0)
            return;

        TriggerEnter?.Invoke(c);
    }

    void OnCollisionEnter(Collision c)
    {
        if ((1 << c.gameObject.layer & CollidesWith) == 0)
            return;

        Enter?.Invoke(c);
    }
}
