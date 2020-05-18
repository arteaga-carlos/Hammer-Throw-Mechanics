using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "Wizard1/AbilityData/TransitionIndexer")]
public class TransitionIndexer : StateData {


    // animator has an int value. This value could be any integer. we can make transition conditions based on the integer we set
    // e.g: transition arrow to jump when the integer is 5
    public int Index;

    private Control control;

    public List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>();

    public enum TransitionConditionType {
        // Add as needed
        NONE,
        HAMMER_EQUIPPED,
        WALK,
        END
    }





    public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo) {

        control = characterStateBase.getController(animator);
    }

    public override void updateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo) {

        if (canMakeTransition()) {            

            // Index is publicly set. It's the value our condition will respond to
            animator.SetInteger(AnimationTags.TRANSITION_INDEX, Index);
        }
    }



    private bool canMakeTransition() {

        // If all conditions are met we return true. If not it will break
        foreach (TransitionConditionType t in transitionConditions) {

            // we publicly set the needed conditions. these must match what is set on the character Control
            switch (t) {
                case TransitionConditionType.HAMMER_EQUIPPED: {

                        if (!control.hammerEquipped) return false;

                    }
                    break;
                // case
            }
        }

        return true;
    }




    public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo) {
        //
    }
}
