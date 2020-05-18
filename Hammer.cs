using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    private Control control;

    private Transform Player;
    private Transform hammerHolder;
    private Transform headCollider;
    private Transform rightHand;
    public Transform ExitRotation;

    private Collider collider;
    private Rigidbody rigidBody;

    public bool collidedWithWorld;
    public bool isEquipped;
    public bool recall;
    public bool isFlying;
    public bool canRecover;

    [Range(100, 2000)]
    public int force;
    public float velocityFactor = 10f;
    public float recallSpeed = 10f;



    // Start is called before the first frame update
    void Awake()
    {
        // get player transform
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        // get control component
        control = Player.GetComponent<Control>();

        // get transform for the hand
        rightHand = getObjectTransform(Player, "mixamorig:RightHand");        
        // get hammer landing position slot's transform
        hammerHolder = getObjectTransform(Player, "hammerHolder");

        // get Collider
        collider = transform.GetComponent<Collider>();

        // get rigidBody
        rigidBody = transform.GetComponent<Rigidbody>();

        recall = false;
        isFlying = false;
        isEquipped = false;
        collidedWithWorld = false;
    }




    // Update is called once per frame
    void Update()
    {
        if (recall) {
            hammerReturn();
        }

        // Stop Hammer flight if collision with world or distance from player is large
        var distanceFromPlayer = (transform.position - hammerHolder.position).sqrMagnitude;
        if (collidedWithWorld || distanceFromPlayer > 4000f) {
            resetPosition();
        }
    }




    public void hammerReturn() {

        // vars      
        collidedWithWorld = false;
        //collider.isTrigger = true;


        // increase speed with time
        recallSpeed += recallSpeed * Time.deltaTime;


        // not equipped
        if (!isEquipped) {

            // set parent
            if (transform.parent == null) { transform.parent = hammerHolder; }

            // reset
            rigidBody.isKinematic = false;

            // move
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, recallSpeed);

            // rotate
            var targetPoint = hammerHolder.transform.position;
            var targetRotation = Quaternion.LookRotation(targetPoint - transform.localPosition, Vector3.up);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, 1f);

        // equipped
        } else {
            recall = false;
            recallSpeed = 0f;
        }
    }



    public void throwHammer() {

        isEquipped = false;
        isFlying = true;

        // Rigidbody
        rigidBody.isKinematic = false;
        rigidBody.useGravity = false;
        rigidBody.AddForce(control.transform.root.forward * force, ForceMode.Impulse);
        // adding the forward vector on every frame to increase velocity
        rigidBody.velocity += control.transform.root.forward * 10f;

        // Collider
        collider.isTrigger = true;

        Vector3 axes = transform.rotation.eulerAngles;

        // unparent
        if (transform.parent != null) {

            // rotate horizontally on hand exit. rotate once using local rotation before unparenting
            transform.rotation = ExitRotation.rotation;

            transform.parent = null;            
        }
    }



    public void resetPosition() {

        rigidBody.isKinematic = true;
        isFlying = false;
    }




    private void OnTriggerEnter(Collider enteredCollider) {

        // Ignore collisions with main objects
        if (enteredCollider.gameObject.name == "Player") return;
        if (enteredCollider.gameObject.name == "Enemy1") return;

        // Collided with hand place-holder
        if (enteredCollider.gameObject.name == hammerHolder.gameObject.name) {

            isEquipped = true;
            isFlying = false;
            recall = false;

            rigidBody.isKinematic = true;

            return;
        }

        collidedWithWorld = true;
        rigidBody.isKinematic = true;
        collider.isTrigger = true;
    }





    // find the empty gameObject containing the hand position for hte hammer
    private Transform getObjectTransform(Transform Player, string name) {

        foreach (Transform childTransform in Player.GetComponentsInChildren<Transform>()) {
            
            if (childTransform.gameObject.name == name) {

                return childTransform;
            }
        }

        return null;
    }
}
