using UnityEngine;
using UnityEngine.InputSystem;

public class animationControler : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isAttackingHash;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isAttackingHash = Animator.StringToHash("isAttacking");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Keyboard.current.wKey.isPressed;
        bool attackPressed = Keyboard.current.fKey.wasPressedThisFrame;
        
        if(forwardPressed)
            walk_animation(true);
        else
            walk_animation(false);

        if(attackPressed)
            attack_animation(true);
        else
            attack_animation(false);
    }

    public void walk_animation(bool shouldWalk){
        bool isWalking = animator.GetBool(isWalkingHash);

        if(!isWalking && shouldWalk)
        {
            animator.SetBool(isWalkingHash,true);
        }
        if (isWalking && !shouldWalk)
        {
            animator.SetBool(isWalkingHash,false);
        }
    }

    public void attack_animation(bool shouldAttack){
        bool isAttacking = animator.GetBool(isAttackingHash);

        if(shouldAttack && !isAttacking)
        {
            animator.SetBool(isAttackingHash,true);
        } 
        else
            animator.SetBool(isAttackingHash,false);
    }
}
