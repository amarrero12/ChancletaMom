using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;

    public bool useOffsetValues;

    public float rotateSpeed;

    public Transform pivot;

    public float maxViewAngle;
    public float minViewAngle;

    public bool invertY;

    // Start is called before the first frame update
    void Start()
    {
        //same as "if (useOffsetValues == false)"
        //allows us to decide whether we want to use our own predetermined camera offset values or just the starting distance
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        //puts the pivot wherever the target(player) currently is
        pivot.transform.position = target.transform.position;
        //right away make the pivot a child of the player so that wherever the player moves, the pivot will move along with it
        //Commented out for alternative in first line LateUpdate
        //pivot.transform.parent = target.transform;
        //stops camera and pivot from spinning together like crazy by removing itself from having a parent in the editor
        //we do it this way instead of removing it in the editor manually so that in the future we can use the same set up with the camera as a prefab and the pivot will work off of this script
        pivot.transform.parent = null;

        //centers and hides the mouse cursor upon starting the game
        //Cursor.lockState = CursorLockMode.Locked;
    
    }

    // Update is called once per frame
    //LateUpdate happens after Update duh (every other scripts update)
    void LateUpdate()
    {

        //Makes the pivot always follow the player rather than making it a child at the start so we can freely turn the camera without following the player (first step)
        pivot.transform.position = target.transform.position;
        //pivot.transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z); 


        //Get the X positiion of the mouse, apply it to a variable (horizontal) & then rotate the target (player)
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        //Edit: switched target.Rotate to pivot.Rotate so we rotate the camera freely not the target (second step)
        pivot.Rotate(0, horizontal, 0);

        //Get the Y position of the mouse and rotate the pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        //negative vertical so the camera looks up when mouse up and down when mouse down
        //remove negative to invert it
        //pivot.Rotate(-vertical, 0, 0);
        //this bool will enable and disable inverted Y camera
        if (invertY)
        {
            pivot.Rotate(vertical, 0, 0);
        } else
        {
            pivot.Rotate(-vertical, 0, 0);
        }

        //Limit up/down camera rotation
        //the editor and the script operate on different kinds of rotation angles. Using >45 stops the camera from going too high and flipping, using <180 allows us 
        //to go below the normal height (0 on the editor), because the editor starting at the avg height goes from 0 to 180 and 0 to -180 but the script just goes 0 to 360
        //We're using maxViewAngle and MinViewAngle so we can freely change the value as we go; maxView being how high we want the camera to go up and MinView being how low
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }
        //Going below 0 gets us into 360 and less, so we want to subtract 45 from 360 which gets us in 315 and we only want to be in the zone above 315 and below 180 (gamesplusjames #7 11:04)
        //We're adding minView to 360 because it makes more sense if we use a negative number in the editor so it can subtract from 360, determining how low we can go
        if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            //this will lock us in place and prevent the camera from going lesser than the value causing us to flip
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }

        //eulerAngles lets us convert the 4 axis quaternion numbers back to the 3 axis numbers from debug rotation
        //Move the camera based on the current rotation of the target & the original offset
        //We're taking the player's angle from the 4 axis debug rotation and storing it in the float
        //float desiredYAngle = target.eulerAngles.y;
        //switched target for pivot to freely rotate camera(third step)
        float desiredYAngle = pivot.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        //Now we're using that value to create our rotation for the camera so it can copy the player's y and x rotation
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        //Now use this new way to move the camera and follow the player so that we can rotate as well by multiplying the offset with the rotation,
        //unlike the former way (next line of code that's commented out)
        transform.position = target.position - (rotation * offset);

        //allows the camera to physically follow player from its starting distance
        //transform.position = target.position - offset;

        //makes the camera not go below the height of the player
        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }

        //takes the transform of current object (camera) and lets it rotate around an object
        //camera is always looking at the player
        transform.LookAt(target);
    }
}
