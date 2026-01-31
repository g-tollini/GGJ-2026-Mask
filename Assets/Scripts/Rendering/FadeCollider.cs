using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FadeCollider : MonoBehaviour
{
    public SphereCollider sphere;

    void Reset() => sphere = GetComponent<SphereCollider>();
    void Awake()
    {
        if (sphere == null) sphere = GetComponent<SphereCollider>();
    }

    public bool ComputePenetration(BoxCollider box, out Vector3 direction, out float distanceNormalized)
    {
        bool penetration = Physics.ComputePenetration(
            sphere, transform.position, transform.rotation,
            box, box.transform.position, box.transform.rotation,
            out direction, out var distance);

        if (penetration)
            distanceNormalized = distance / Mathf.Max(sphere.radius, 1);
        else
            distanceNormalized = 0f;

            return penetration;
    }
}
