using UnityEngine;
using UnityEngine.InputSystem;

public class NPC_animationController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isAttackingHash;
    int isDeadHash;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isAttackingHash = Animator.StringToHash("isAttacking");
        isDeadHash = Animator.StringToHash("isDead");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isAttacking = animator.GetBool(isAttackingHash);
        bool isDead = animator.GetBool(isDeadHash);

        bool walkAction = Keyboard.current.wKey.isPressed;     //mettre ici ce qui active l'anime de marche
        bool attackAction = Keyboard.current.fKey.wasPressedThisFrame;   //mettre ici ce qui active l'anime d'attack
        bool deathAction = Keyboard.current.qKey.isPressed;     //mettre ici ce qui active l'anime de mort

        if(deathAction)
        {
            animator.SetBool(isDeadHash,true);
        }
        else
            animator.SetBool(isDeadHash,false);
        
        if(walkAction && !isWalking)
        {
            animator.SetBool(isWalkingHash,true);
        }
        if(!walkAction && isWalking)
        {
            animator.SetBool(isWalkingHash,false);
        }

        if(attackAction)
        {
            animator.SetBool(isAttackingHash,true);
            isAttacking = false;
        } 
        else
            animator.SetBool(isAttackingHash,false);

        
    }
}
