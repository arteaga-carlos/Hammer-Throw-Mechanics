using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "Wizard1/AbilityData/MoveForward")]

public class MoveForward : StateData {


    public float speed = 2f;
    public bool constant = false;

    public AnimationCurve speedGraph;


    private Control control;

    public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
    {
        animator.SetBool(AnimationTags.END, false);
    }

    public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
    {
        //
    }


    // MoveForward is a StateData; which is an item from a list of abilities.
    // CharacterStateBase, which is attached to the object, will call updateAbility from all StateData
    public override void updateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
    {

        // get control script component
        control = characterStateBase.getController(animator);


        // if moving left
        if (control.moveLeft)
        {
            // animate walk
            animator.SetBool(AnimationTags.WALK, true);

            // move left
            control.transform.Translate(Vector3.forward * speed * speedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime);

            // rotate left
            control.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
        }



        // if moving right
        if (control.moveRight)
        {
            animator.SetBool(AnimationTags.WALK, true);

            control.transform.Translate(Vector3.forward * speed * speedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime);

            control.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        }



        // not moving
        if (!control.moving)
        {
            animator.SetBool(AnimationTags.WALK, false);
        }



        // move forward regarless of input. Uses include moving forward with a punch
        if (constant)
        {
            control.transform.Translate(Vector3.forward * speed * speedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime);
        }
    }
}
