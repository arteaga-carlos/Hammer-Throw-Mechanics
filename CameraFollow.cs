using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition;

    // Start is called before the first frame update
    void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        float newPos = target.position.x;

        transform.position = new Vector3(newPos, offsetPosition.y, offsetPosition.z); ;
    }
}
