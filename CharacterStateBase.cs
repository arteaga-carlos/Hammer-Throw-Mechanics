using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateBase : StateMachineBehaviour
{

    public List<StateData> listAbilityData;
    private Control control;





    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (StateData sd in listAbilityData)
        {
            sd.OnEnter(this, animator, stateInfo);
        }
    }




    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        updateAll(animator, stateInfo);
    }




    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (StateData sd in listAbilityData)
        {
            sd.OnExit(this, animator, stateInfo);
        }
    }




    public void updateAll(Animator animator, AnimatorStateInfo stateInfo)
    {
        foreach (StateData sd in listAbilityData)
        {
            sd.updateAbility(this, animator, stateInfo);
        }
    }



    public Control getController(Animator animator)
    {
        if (control == null)
        {
            control = animator.GetComponentInParent<Control>();
        }

        return control;
    }
}
