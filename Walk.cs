using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "Wizard1/AbilityData/Walk")]

public class Walk : StateData {

    private Control control;

    public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo) {

        control = characterStateBase.getController(animator);
    }




    public override void updateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo) {


        // move left
        if (control.moveLeft || control.moveRight) {

            animator.SetBool(AnimationTags.WALK, true);

        } else {

            animator.SetBool(AnimationTags.WALK, false);
        }

        // switch to attack
        if (control.attack) {

            animator.SetBool(AnimationTags.LEFT_SWING, true);
        }

        // switch to recall
        if (control.recall) {

            animator.SetBool(AnimationTags.RECALL, true);
        }


        // Hammer throw
        if (control.throwHammer && control.hammerEquipped) {

            animator.SetBool(AnimationTags.THROW, true);
        }
    }




    public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo) {
        
        //
    }
}
