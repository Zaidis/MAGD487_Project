using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionBehavior : StateMachineBehaviour
{
    public PlayerAnimationController PAC;
    private SpriteRenderer SR;
    private RaycastHit2D hit;
    [SerializeField] private int animNum;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerMovement.instance.canMove = true;
        PAC = GameObject.Find("Graphic").GetComponent<PlayerAnimationController>();
        SR = GameObject.Find("Graphic").GetComponent<SpriteRenderer>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (Attack.instance.AttackInputReceived) {
            if(PAC.wt == weaponType.sword && animNum == 2) //Stops player from teleporting into wall genius code, not spaghetti
            {
                hit = Physics2D.Raycast(SR.transform.position, Vector2.right * (SR.flipX ? -1f : 1f));
                if(hit.distance >= 3)
                {
                    animator.SetTrigger("Attack" + animNum);
                    Attack.instance.InputManager();
                    Attack.instance.AttackInputReceived = false;
                }
            }
            else
            {
                animator.SetTrigger("Attack" + animNum);
                Attack.instance.InputManager();
                Attack.instance.AttackInputReceived = false;
            }            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerMovement.instance.canMove = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
