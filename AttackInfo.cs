using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInfo : Singleton<AttackManager>
{
    public Control attackerControl = null;
    public Attack attackAbility;
    public List<string> colliderNames = new List<string>();

    public bool mustCollide;
    public bool mustFaceAttacker;
    public bool isRegistered;
    public bool isFinished;

    public float lethalRange;

    public int maxHits;
    public int currentHits;



    public void resetAttackInfo(Attack attack) {
        isRegistered = false;
        isFinished = false;
        attackAbility = attack;
    }



    public void register(Attack attack, Control control) {

        isRegistered = true;
        attackerControl = control;

        attackAbility = attack;
        colliderNames = attack.colliderNames;
        mustCollide = attack.mustCollide;
        mustFaceAttacker = attack.mustFaceAttacker;
        lethalRange = attack.lethalRange;
        maxHits = attack.maxHits;
        currentHits = 0;
    }
}
