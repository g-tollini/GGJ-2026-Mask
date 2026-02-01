using UnityEngine;
using UnityEngine.InputSystem;

public class animationControler : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isAttackingHash;

    public InputActionReference walkAction;
    public InputActionReference attackAction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isAttackingHash = Animator.StringToHash("isAttacking");

        walkAction.action.performed += Walk;
        walkAction.action.canceled += Walk;
        attackAction.action.started += Attack;
        attackAction.action.canceled += Attack;
        walkAction.action.Enable();
        attackAction.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Walk(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
            walk_animation(true);
        if (ctx.phase == InputActionPhase.Canceled)
        {
            walk_animation(false);
            Debug.Log("ici");
        }
            
    }

    void Attack(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Started)
            attack_animation(true);
        if (ctx.phase == InputActionPhase.Canceled)
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
        else if (!shouldAttack && isAttacking)
            animator.SetBool(isAttackingHash,false);
    }
}
