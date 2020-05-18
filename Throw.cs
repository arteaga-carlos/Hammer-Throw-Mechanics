using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "Wizard1/AbilityData/Throw")]

public class Throw : StateData{

    private Control control;
    private GameObject Hammer;
    private AttackInfo attackInfo;
    private Hammer HammerScript;



    public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo) {

        control = characterStateBase.getController(animator);
        Hammer = control.Hammer;
        HammerScript = Hammer.GetComponent<Hammer>();
    }
    public override void updateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo) {
        
       //
    }


    public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo) {

        // if inside the 'throw hammer begin' state. throw hammer
        if (control.hammerEquipped) {

            HammerScript.throwHammer();

            // camera shake
            //CameraManager.Instance.CameraShake(0.03f);

            // if inside the 'throw hammer end' state. throw hammer
        } else {

            control.hammerEquipped = false;

            animator.SetBool(AnimationTags.THROW, false);
        }
    }
}
