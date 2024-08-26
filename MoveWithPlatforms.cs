using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class MoveWithPlatforms : MonoBehaviour
{
    private Rigidbody rbody;
    private bool isOnPlatform;
    private Rigidbody platformRBody;
    private void Awake()
    {
        rbody = GetComponent<Rigidbody>();
    }
 
    void FixedUpdate()
    {
        if(isOnPlatform)
        {
            rbody.velocity = rbody.velocity + platformRBody.velocity;
        }
    }
 
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Platform")
        {
            platformRBody = col.gameObject.GetComponent<Rigidbody>();
            isOnPlatform = true;
        }
    }
 
    void OnCollisionExit(Collision col)
    {
        if(col.gameObject.tag == "Platform")
        {
            isOnPlatform = false;
            platformRBody = null;
        }
    }
}
