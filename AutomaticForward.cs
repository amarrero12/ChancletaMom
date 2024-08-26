using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticForward : MonoBehaviour
{
    public PlayerController playerScript;
    public bool autoRun;
    public float newSpeed;
    public Vector3 moveDirection;
    public float moveSpeed;
    public float gravityScale;
    public CharacterController controller;
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        //autoRun = true;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(autoRun == true)
        // {
        //     playerScript.moveDirection = (transform.forward * newSpeed) + (transform.right * Input.GetAxisRaw("Horizontal") * playerScript.moveSpeed);
        // }

        float yStore = moveDirection.y;
        moveDirection = (transform.forward * newSpeed) + (transform.right * Input.GetAxisRaw("Horizontal") * moveSpeed);
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (controller.isGrounded)
        {   
            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
        }
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
