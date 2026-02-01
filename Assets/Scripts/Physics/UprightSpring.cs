using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UprightSpringLimited : MonoBehaviour
{
    [Tooltip("Force de rappel vers 'up'. Plus haut = plus raide.")]
    public float strength = 30f;

    [Tooltip("0 = très stable (beaucoup d'amortissement), 1 = très wobble (peu d'amortissement).")]
    [Range(0f, 1f)]
    public float wobbliness = 0.6f;

    [Tooltip("Au-delà de cet angle (en degrés), le script n'aide plus à se relever.")]
    [Range(0f, 180f)]
    public float maxRecoverAngle = 35f;

    Rigidbody rb;

    void Awake() => rb = GetComponent<Rigidbody>();

    void FixedUpdate()
    {
        float tilt = Vector3.Angle(transform.up, Vector3.up);

        // Trop penché => on ne corrige plus (il tombe, ou reste comme il est)
        if (tilt > maxRecoverAngle)
            return;

        // Axe/angle de correction
        Vector3 axis = Vector3.Cross(transform.up, Vector3.up);
        float axisMag = axis.magnitude;
        if (axisMag < 0.0001f) return;

        float angleRad = Mathf.Asin(Mathf.Clamp(axisMag, -1f, 1f));
        Vector3 dir = axis / axisMag;

        float damping = Mathf.Lerp(strength * 2.0f, strength * 0.1f, wobbliness);

        Vector3 torque = dir * (angleRad * strength) - rb.angularVelocity * damping;
        rb.AddTorque(torque, ForceMode.Acceleration);
    }
}
