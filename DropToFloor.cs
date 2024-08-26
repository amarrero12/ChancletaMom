using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropToFloor : MonoBehaviour
{
    public Rigidbody rb;
    public bool bounceMode;
    public bool followUs;
    public Transform playerTransform;
    Vector3 pos;
    public float followY;
    public float fallZ;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        followY = 8;
    }

    // Update is called once per frame
    void Update()
    {
        //pos = new Vector3(transform.position.x, followY, playerTransform.position.z + 25);
        if (bounceMode == true)
        {
            followY = transform.position.y;
            //fallZ = transform.position.z;
            rb.useGravity = true;
            followUs = false;
        }

        if(followUs == true)
        {
            //fallZ = playerTransform.position.z + 25;
            pos = new Vector3(transform.position.x, followY, playerTransform.position.z + 25);
            transform.position = pos;
        }

        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "lvl2Cat2")
        {
            bounceMode = true;
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "mydestroyer")
        {
            Debug.Log("hellllo");
            pos = new Vector3(0, 12, playerTransform.position.z + 35);
            transform.position = pos;
            //rb.AddForce(0, 0, 1000);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "bouncyfloor")
        {
            rb.AddForce(0, 450, 0);
        }

        
    }
}
