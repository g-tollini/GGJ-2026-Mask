using UnityEngine;
using UnityEngine.InputSystem;

public class animationControler : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isAttackingHash;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isAttackingHash = Animator.StringToHash("isAttacking");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isAttacking = animator.GetBool(isAttackingHash);

        bool forwardPressed = Keyboard.current.wKey.isPressed;
        bool runPressed = Keyboard.current.leftShiftKey.isPressed;
        bool attackPressed = Keyboard.current.fKey.isPressed;
        
        if(!isWalking && forwardPressed && !attackPressed)
        {
            animator.SetBool(isWalkingHash,true);
        }
        if (attackPressed || (isWalking && !forwardPressed))
        {
            animator.SetBool(isWalkingHash,false);
        }

        if(!isRunning && !attackPressed && (forwardPressed && runPressed))
        {
            animator.SetBool(isRunningHash,true);
        }

        if(attackPressed || (isRunning && (!forwardPressed || !runPressed)))
        {
            animator.SetBool(isRunningHash,false);
        }

        if(attackPressed)
        {
            animator.SetBool(isAttackingHash,true);
            isAttacking = false;
        } 
        else
            animator.SetBool(isAttackingHash,false);
        /*if(!isAttacking && attackPressed)
        {
            animator.SetBool(isAttackingHash,true);
            //animator.SetBool(isRunningHash,false);
            //animator.SetBool(isWalkingHash,false);
        }
        if(isAttacking && !attackPressed)
        {
            animator.SetBool(isAttackingHash,false);
        }*/
    }
}
