using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "Wizard1/AbilityData/ForceTransition")]


// The class function is as it reads. We can force a transition to another state by updating the one paramater called forceTransition in the animator to true;
// then set it to false once the animation ends.
public class ForceTransition : StateData
{

    [Range(0.01f, 1f)]
    public float transitionTiming;


    private Control control;

    public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
    {
        //
    }




    // CharacterStateBase, which is attached to the object, will call updateAbility from all StateData
    public override void updateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
    {

        // The normalized time of the animation. A value of 1 is the end of the animation. A value of 0.5 is the middle of the animation.
        if (stateInfo.normalizedTime >= transitionTiming)
        {

            // change "ForceTransition" to an enum value
            animator.SetBool("forceTransition", true);
        }
    }




    public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool("forceTransition", false);
    }
}
