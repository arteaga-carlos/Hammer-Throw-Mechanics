using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Will switch animator after the end of an animation; based on chosen timing.
 * 
*/

[CreateAssetMenu(fileName = "New Scriptable Object", menuName = "Wizard1/AbilityData/SwitchAnimator")]
public class SwitchAnimator : StateData {


    public RuntimeAnimatorController targetAnimator;
    public float switchTiming;


    public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo) {
        //
    }

    public override void updateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo) {

        // after elapsed time, switch animator held in Control
        if (stateInfo.normalizedTime >= switchTiming) {

            characterStateBase.getController(animator).GetComponent<Animator>().runtimeAnimatorController = targetAnimator;
        }
    }

    public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo) {
        //
    }
}
