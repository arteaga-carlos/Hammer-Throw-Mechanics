using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public bool isPlayer;
    public bool moveLeft;
    public bool moveRight;
    public bool moving;
    public bool attack;
    public bool recall;
    public bool throwHammer;
    public bool hammerEquipped;
    public bool dead;

    private float rightClickTimer = 0f;
    private float maxRightClickHeldTime = 0.7f;

    // list of body parts coming in contact
    public List<Collider> enteredColliders = new List<Collider>();
    public List<Collider> listRagdollColliders = new List<Collider>();
    public Rigidbody[] listRagdollRigidBodies;
    public GameObject Hammer;
    private Rigidbody rigid;
    private Hammer hammerScript;






    void Awake()
    {

        // only enable manual input for player
        if (this.gameObject.tag == "Player") { isPlayer = true; } else { isPlayer = false; }


       hammerScript = Hammer.GetComponent<Hammer>();



        // ragdoll is disabled while animator is active. 
        disableRagDoll();



        // get info from box character's box collider we can use to detect ground collision
        /*BoxCollider box = GetComponent<BoxCollider>();

        float boxColliderbottom = box.bounds.center.y - box.bounds.extents.y;
        float boxCollidertop = box.bounds.center.y + box.bounds.extents.y;
        float boxColliderfront = box.bounds.center.z + box.bounds.extents.z;
        float boxColliderback = box.bounds.center.z - box.bounds.extents.z;
        */
    }


    // Update is called once per frame
    void Update()
    {
        // enable controls for player only
        if (isPlayer) {

            // ----------- Move ----------- //

            // move forwards
            if (Input.GetKey(KeyCode.Q)) {
                moving = true;
                moveLeft = true;
                moveRight = false;

                // move backwards
            } else if (Input.GetKey(KeyCode.E)) {
                moving = true;
                moveLeft = false;
                moveRight = true;

                // if no input
            } else {
                moving = false;
                moveLeft = false;
                moveRight = false;
            }

            // -----------  ----------- //



            // ----------- Attack ----------- //

            // attack; left click
            if (Input.GetKey(KeyCode.Mouse0)) {
                attack = true;
            } else {
                attack = false;
            }

            // -----------  ----------- //




            // ----------- Hammer ----------- //

            // recall Hammer with right mouse button
            if (Input.GetKey(KeyCode.Mouse1)) {

                // cannot hold down key for too long
                if (rightClickTimer < maxRightClickHeldTime) {

                    rightClickTimer += Time.deltaTime;

                    // if equipped, throw. If not, recall
                    if (hammerScript.isEquipped) {

                        throwHammer = true;
                    } else {

                        recall = true;
                    }

                    // past mouse held allowed time. reset
                } else {

                    throwHammer = false;
                }

            } else {
                recall = false;
                throwHammer = false;

                rightClickTimer = 0f;
            }


            // Hammer
            if (hammerScript.isEquipped) {
                hammerEquipped = true;
                recall = false;
            } else {
                hammerEquipped = false;
            }

            // -----------  ----------- //
        }

    } // update




    // whenever something enters our ragdoll body parts
    // Docs: Called when a gameobject collides with the other. Both Objects must have a collider. One must have Collider.isTrigger enabled, and contain a Rigidbody.  
    private void OnTriggerEnter(Collider col) {

        // if the part triggering the collision belongs to the same ragdoll
        if (listRagdollColliders.Contains(col)){
            return;
        }


        // add colliding parts to list
        if (!enteredColliders.Contains(col)) {

            enteredColliders.Add(col);
        }

        /*// if the owner of this collider doesn't have a Control script, it's not a player it's just a physical object
        Control colliderParentControl = col.transform.root.GetComponent<Control>();
        if (colliderParentControl == null){

            return;
        }

        // if the object that triggered the collision collided with itself
        if (col.gameObject.name == colliderParentControl.gameObject.name){

            return;
        }*/

    } // OnTriggerEnter



    // not colliding anymore
    private void OnTriggerExit(Collider col)
    {
        if (enteredColliders.Contains(col))
        {
            enteredColliders.Remove(col);
        }
    }





    void disableRagDoll()
    {
        // get all colliders and rigidbodies attached to this gameObject, including the ones being used in ragdoll
        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();
        listRagdollRigidBodies = this.gameObject.GetComponentsInChildren<Rigidbody>();

        foreach(Collider collider in colliders)
        {

            // this will set all ragdoll parts to trigger but not our box collider component
            if (collider.gameObject != this.gameObject)
            {
                
                // this basically disables the objects from acting physically and become only an alert when this object collides wih another
                // only enable for our player so OnTriggerEnterFunction engages
                if( isPlayer ) collider.isTrigger = true;

                // save all ragdoll parts to a list
                listRagdollColliders.Add(collider);
            }
        }

        // disables all forces for the rigidbodies attached to the object
        foreach (Rigidbody rb in listRagdollRigidBodies)
        {
           rb.isKinematic = true;
        }
    }





    public Rigidbody RIGID_BODY
    {
        get
        {
            if (rigid == null)
            {
                rigid = GetComponent<Rigidbody>();
            }

            return rigid;
        }
    }
}
