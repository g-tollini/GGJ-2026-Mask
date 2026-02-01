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
        bool walkAction = Keyboard.current.wKey.isPressed;     //mettre ici ce qui active l'anime de marche
        bool attackAction = Keyboard.current.fKey.wasPressedThisFrame;   //mettre ici ce qui active l'anime d'attack
        bool deathAction = Keyboard.current.qKey.isPressed;     //mettre ici ce qui active l'anime de mort

        walk_animation(walkAction);
        attack_animation(attackAction);
        death_animation(deathAction);
    }

    public void walk_animation(bool shoudlWalk)
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        
        if(shoudlWalk && !isWalking)
        {
            animator.SetBool(isWalkingHash,true);
        }
        if(!shoudlWalk && isWalking)
        {
            animator.SetBool(isWalkingHash,false);
        }
    }

    public void attack_animation(bool shouldAttack)
    {
        bool isAttacking = animator.GetBool(isAttackingHash);
        
        if(shouldAttack)
        {
            animator.SetBool(isAttackingHash,true);
        } 
        else
            animator.SetBool(isAttackingHash,false);


    }

    public void death_animation(bool shoudDie)
    {
        bool isDead = animator.GetBool(isDeadHash);

        if(shoudDie && !isDead)
        {
            animator.SetBool(isDeadHash,true);
        }
        else
            animator.SetBool(isDeadHash,false);
        
    }
}
