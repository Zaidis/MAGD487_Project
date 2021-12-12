using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class katanaExitHandler : StateMachineBehaviour
{
    public PlayerAnimationController PAC;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PAC = GameObject.Find("Graphic").GetComponent<PlayerAnimationController>();
        SpriteRenderer SR = GameObject.Find("Graphic").GetComponent<SpriteRenderer>();
        if (PAC.wt == weaponType.sword)
        {
            if(!SR.flipX)
            {
                PlayerMovement.instance.transform.position = new Vector2(PlayerMovement.instance.transform.position.x + 2.5f, PlayerMovement.instance.transform.position.y);
            }
            else
            {
                PlayerMovement.instance.transform.position = new Vector2(PlayerMovement.instance.transform.position.x - 2.5f, PlayerMovement.instance.transform.position.y);
            }
            //move player correct distance based on attack direction
        }
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
