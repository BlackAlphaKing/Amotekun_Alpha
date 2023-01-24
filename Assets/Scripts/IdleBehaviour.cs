using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   // override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
              // ComboManager.instance.canRecieveInput = true;

   //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      if (PlayerScript.instance.isAttacking)
      {
         // animator.SetTrigger("AttackOne");
          PlayerScript.instance.CharacterAnimator.Play("attack1");
      }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerScript.instance.isAttacking = false;
    }
    
}
