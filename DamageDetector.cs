using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{

    Control control;
    Hammer Hammer;

    // Start is called before the first frame update
    void Awake() {

        // get control script for current game person
        control = GetComponent<Control>();
        Hammer = control.Hammer.GetComponent<Hammer>();
    }

    // Update is called once per frame
    void Update()
    {
        // if hammer is flying, check for collisions
        if (Hammer.isFlying) {

            checkAttack();

        // attacks have been registered in attack manager
        } else if (AttackManager.Instance.currentAttacks.Count > 0) {

            checkAttack();
        }
    }


    private void checkAttack() {

        // if character object contains a Hammer collision
        foreach (Collider collider in control.enteredColliders) {

            if (collider.gameObject.name == "Hammer") {

                //takeDamage();
            }
        }

        // Check Attack info attacks
        foreach (AttackInfo attackInfo in AttackManager.Instance.currentAttacks) {

            // AttackManager could have empty list elements, skip them
            if (attackInfo == null) {

                continue;
            }

            // if attack is not registered, skip
            if (!attackInfo.isRegistered) {

                continue;
            }

            // attack animation is finished, skip
            if (attackInfo.isFinished) {

                continue;
            }


            if (attackInfo.mustCollide) {

                foreach (string colliderName in attackInfo.colliderNames) {

                    if (colliderName == "HeadCollider") {

                        if (hasCollided(colliderName)) {

                            //Debug.Log("Hammer hit!");

                            control.GetComponent<Animator>().runtimeAnimatorController=ManagerReactionAnimations.Instance.getAnimator(GeneralBodyParts.HAMMER);        
                        }
                    }
                }
            }
        }
    } // function



    private bool hasCollided(string colliderName) {

        // fix collidedPart
        foreach (Collider collider in control.enteredColliders) {

            // if the name we specified in the AttackInfo object is also part of the currently colliding parts, we have a hit
            if (colliderName == collider.gameObject.name) {

                return true;
            }
        }

        return false;
    }


    private void takeDamage() {

        control.dead = true;
    }

} // class
