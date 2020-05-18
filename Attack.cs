using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "Wizard1/AbilityData/Attack")]

public class Attack : StateData
{
       
    private Control control;

    public float startAttackTime;
    public float endAttackTime;
    public float lethalRange;

    public bool mustCollide;
    public bool mustFaceAttacker;
    private bool combo;

    public int maxHits;

    public List<string> colliderNames = new List<string>();



    public override void OnEnter(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
    {

        // get Control script component attached to our gameObject
        control = characterStateBase.getController(animator);


        // attack kicked off, now disable
        animator.SetBool(AnimationTags.LEFT_SWING, false);
        animator.SetBool(AnimationTags.RIGHT_SWING, false);
        animator.SetBool(AnimationTags.END, false);


        // Load prefab empty object that contains the attack info script
        // We can use this object as a way to broadcast information on attacks
        GameObject obj = Instantiate(Resources.Load("AttackInfo", typeof(GameObject))) as GameObject;
        AttackInfo attackInfo = obj.GetComponent<AttackInfo>();
        attackInfo.resetAttackInfo(this);

        // Add object containing attack info into the attack manager
        if ( ! AttackManager.Instance.currentAttacks.Contains(attackInfo)) {

            AttackManager.Instance.currentAttacks.Add(attackInfo);
        }
    }



    // CharacterStateBase, which is attached to the object, will call updateAbility from all StateData. This will run while the animation lasts.
    public override void updateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
    {
        registerAttack(characterStateBase, animator, stateInfo);
        deRegisterAttack(characterStateBase, animator, stateInfo);

        // if player presses attack again he might be able to trigger a combo
        if (control.attack) {

            if (
                stateInfo.normalizedTime > startAttackTime &&
                stateInfo.normalizedTime < endAttackTime) {
                animator.SetBool(AnimationTags.RIGHT_SWING, true);
            }

            // time window on which combo can be triggered
            if ( 
                stateInfo.normalizedTime > startAttackTime && 
                stateInfo.normalizedTime < endAttackTime &&
                stateInfo.normalizedTime > (startAttackTime + endAttackTime / 2) 
            ) {

                combo = true;
            }
        }

        // Transition to Right Swing Combo
        if(combo) animator.SetBool(AnimationTags.RIGHT_SWING, true);


        // if moving switch to Walk
        if (stateInfo.normalizedTime > 0.7f && control.moving) {

            animator.SetBool(AnimationTags.END, true);
        }
    }



    public void registerAttack(CharacterStateBase characterState, Animator animator, AnimatorStateInfo stateInfo) {

        // register attack if our start attack time is less than the end of the animation and our end time is bigger than the animation length
        if ( startAttackTime <= stateInfo.normalizedTime && endAttackTime > stateInfo.normalizedTime ) {

            // cycle through all attack info objects
            foreach (AttackInfo attackInfo in AttackManager.Instance.currentAttacks) {

                // fail safe
                if (attackInfo == null) {

                    continue;
                }

                // register only the attack associated with this attack object
                if ( ! attackInfo.isRegistered && attackInfo.attackAbility == this ) {

                    attackInfo.register(this, control);
                }
            }
        }
    }



    public void deRegisterAttack(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo) {

        if ( stateInfo.normalizedTime >= endAttackTime ) {

            foreach(AttackInfo attackInfo in AttackManager.Instance.currentAttacks) {

                if(attackInfo == null) {

                    continue;
                }

                if(attackInfo.attackAbility == this && !attackInfo.isFinished) {

                    attackInfo.isFinished = true;

                    Destroy(attackInfo.gameObject);
                }
            }
        }
    }



    public void clearAttack() {

        for (int i = 0; i < AttackManager.Instance.currentAttacks.Count; i++) {

            if (AttackManager.Instance.currentAttacks[i] == null || AttackManager.Instance.currentAttacks[i].isFinished) {

                AttackManager.Instance.currentAttacks.Remove(AttackManager.Instance.currentAttacks[i]);
            }
        }
    }


    // will be called when the parameter for this animation is off
    public override void OnExit(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo stateInfo)
    {
        //
    }
}
