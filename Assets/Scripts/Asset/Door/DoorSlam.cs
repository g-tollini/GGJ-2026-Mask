using UnityEngine;
using UnityEngine.InputSystem;

public class DoorSlam : MonoBehaviour
{
    public Rigidbody rb;
    public Collider playerCol;
    void OnTriggerStay(Collider col){
        
        if (Keyboard.current.fKey.isPressed && col == playerCol)
        {
            rb.isKinematic = false;
            rb.linearVelocity = -115* transform.forward;
        }
    }
}
