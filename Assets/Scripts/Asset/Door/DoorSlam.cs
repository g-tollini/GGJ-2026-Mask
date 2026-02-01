using UnityEngine;
using UnityEngine.InputSystem;

public class DoorSlam : MonoBehaviour
{
    public Rigidbody rb;
    void OnTriggerStay(Collider col){
        
        if (Keyboard.current.fKey.isPressed && col.tag == "Player")
        {
            rb.isKinematic = false;
            rb.linearVelocity = -115* transform.forward;
        }
    }
}
