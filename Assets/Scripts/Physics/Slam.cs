using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Slam : MonoBehaviour
{
    public enum Mode { Forward, SignedForward, Towards }

    public Transform target;
    public Mode mode = Mode.Forward;
    public bool triggerOnce = true;
    public float minImpulse = 1f;

    Rigidbody rb;
    bool done;

    public void DoSlam(float impulse = 0f)
    {
        if (triggerOnce && done)
            return;

        if (!done)
        {
            rb = GetComponent<Rigidbody>();
            if (!rb.constraints.Equals(RigidbodyConstraints.None))
                rb.constraints = RigidbodyConstraints.None;
        }

        var targetDir = Vector3.zero;
        if (target != null)
            targetDir = (target.position - transform.position).normalized;

        var dir = transform.forward;
        if (targetDir.sqrMagnitude != 0 && mode == Mode.Towards)
            dir = targetDir;
        else if (mode == Mode.SignedForward)
            dir = -Vector3.Dot(dir, targetDir) * dir;

        rb.linearVelocity += dir * Mathf.Max(impulse, minImpulse);
        done = true;
    }


    public void CollideSlam(Collider col)
    {
        if (triggerOnce && done)
            return;

        var impulse = col.attachedRigidbody != null ? col.attachedRigidbody.linearVelocity.magnitude : 0;
        var tmp = target;
        target = col.transform;
        DoSlam(impulse);
        target = tmp;
    }
}
