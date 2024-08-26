using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutCamera : MonoBehaviour
{
    public Transform target;
    public Transform newSpotForTarget;
    public Transform player;
    public Transform pivot;
    public bool shiftCam;
    public bool permCam;
    public float howFar;
    public float howHigh;
    public float speed;

    public float distToNewTargetSpot;

    void Update()
    {
        //distToNewTargetSpot = Vector3.Distance(target.position, new Vector3(player.position.x, howHigh, player.position.z - howFar));

        // Vector3 pivotDirection = pivot.position - transform.position;
        // float singleStep = speed * Time.deltaTime;
        // Vector3 newDirection = Vector3.RotateTowards(transform.forward, pivotDirection, singleStep, 5.0f);

        //Change target position so cam can zoom out
        if(shiftCam)
        {
            var step = speed * Time.deltaTime;

            //unparent the target from the player
            //target.transform.SetParent(GameObject.Find("2ndTarget"));

            //target.position = Vector3.MoveTowards(target.position, new Vector3(player.position.x, howHigh, player.position.z - howFar), step);

            //rotate the pivot so the camera rotates
            pivot.rotation = Quaternion.RotateTowards(pivot.rotation, Quaternion.Euler(new Vector3(-12, pivot.rotation.eulerAngles.y, pivot.rotation.eulerAngles.z)), step*2);

            //target.position = new Vector3(player.position.x, howHigh, player.position.z - howFar);
        }

        // if(distToNewTargetSpot <= 0.1f)
        // {
        //     shiftCam = false;
        //     permCam = true;
        // }

        // if(permCam)
        // {
        //     var step = speed * Time.deltaTime;
        //     target.position = new Vector3(player.position.x, howHigh, player.position.z - howFar);
        //     pivot.rotation = Quaternion.RotateTowards(pivot.rotation, Quaternion.Euler(new Vector3(-12, pivot.rotation.eulerAngles.y, pivot.rotation.eulerAngles.z)), step*2);
        // }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shiftCam = true;
            target.position = newSpotForTarget.position;
            target.SetParent(newSpotForTarget);
        }
    }
}
