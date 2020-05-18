using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "Wizard1/AbilityData/Idle")]

public class Idle : StateData{
    

    private Control control;
    private float animationTimer;
    private float throwTransition;


    public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
    {

        animator.SetBool(AnimationTags.RECALL, false);
        animator.SetBool(AnimationTags.THROW, false);
        animator.SetBool(AnimationTags.LEFT_SWING, false);
        animator.SetBool(AnimationTags.RIGHT_SWING, false);
        animator.SetBool(AnimationTags.DEAD, false);
    }


    // Idle is a StateData; which is an item from a list of abilities.
    // CharacterStateBase, which is attached to the object, will call updateAbility from all StateData
    public override void updateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
    {

        // get control component
        control = characterStateBase.getController(animator);


        // move left
        if (control.moveLeft || control.moveRight)
        {
            animator.SetBool(AnimationTags.WALK, true);
        } else
        {
            animator.SetBool(AnimationTags.WALK, false);
        }


        // attack
        if (control.attack)
        {
            // different arm jab triggered depending on forward position
            if (control.transform.forward.x > 1)
            {
                animator.SetBool(AnimationTags.RIGHT_SWING, true);
            }
            else
            {
                animator.SetBool(AnimationTags.LEFT_SWING, true);
            }
        }


        // Hammer recall
        if (control.recall && !control.hammerEquipped) {

            animator.SetBool(AnimationTags.RECALL, true);
        }

        // Hammer throw
        if (control.throwHammer && control.hammerEquipped) {

            animator.SetBool(AnimationTags.THROW, true);
        }


        // Death
        if (control.dead) {
            animator.SetBool(AnimationTags.DEAD, true);
        }
    }




    public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
    {
        //
    }
}
