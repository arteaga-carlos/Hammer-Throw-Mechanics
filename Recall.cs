 using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New State", menuName = "Wizard1/AbilityData/Recall")]
public class Recall : StateData {


    private Control control;
    private Hammer Hammer;

    [Range(0.1f, 3.0f)]
    public float TimeEnableRecall;
    private float TimerRecalling;
    private bool canRecall;


    public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo) {

        TimerRecalling = 0f;
        canRecall = false;

        control = characterStateBase.getController(animator);
        Hammer = control.Hammer.GetComponent<Hammer>();

        // reset previous animation values
        animator.SetBool(AnimationTags.FORCE_TRANSITION, false);
        animator.SetInteger(AnimationTags.TRANSITION_INDEX, 0);
        animator.SetBool(AnimationTags.END, false);
    }



    public override void updateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo) {

        // BEGIN RECALL
        if (canRecall) {

            Hammer.recall = true;
        }

        // control recalling timer
        if (control.recall) { TimerRecalling += Time.deltaTime; }


        // enable recall
        if (control.recall && TimerRecalling > TimeEnableRecall) {            

            canRecall = true;
        
        // not recalling. go Idle
        } else if (!control.recall && !Hammer.isEquipped && TimerRecalling < TimeEnableRecall) {

            animator.SetBool(AnimationTags.RECALL, false);
        }

        // hammer equipped. disable recall
        if (Hammer.isEquipped) {

            Hammer.recall = false;
        }
    }




    public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo) {

        //
    }
}
