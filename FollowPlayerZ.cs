using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerZ : MonoBehaviour
{
    public Transform player;
    public float howFar;
    
    public Transform startSpot;
    public Transform endSpot;
    public Transform topSpot;
    public float speed;

    private float startTime;
    private float journeyLength;

    public bool goBro;
    public bool goGirl;
    private float distToSpot;
    private float distToTopSpot;

    public Transform centerStartSpot;

    // Start is called before the first frame update
    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startSpot.position, endSpot.position);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z - howFar);
        
        distToSpot = Vector3.Distance(transform.position, endSpot.position);
        distToTopSpot = Vector3.Distance(transform.position, topSpot.position);
        if(goBro == true)
        {
            CameraDown();
            if(distToSpot <= .1f)
            {
                goBro = false;
            }

        }
        
        if(goGirl == true)
        {
            CameraUp();
            if(distToTopSpot <= .1f)
            {
                goGirl = false;
            }
        }

        //When player dies, target returns to its down position
        if(GameObject.Find("GameManager").GetComponent<EnemyManager>().nuts == true)
        {
            goBro = true;
            goGirl = false;
            //transform.position = endSpot.transform.position;
        }

        // if (distToSpot <= .5f)
        // {
        //     goBro = false;
        // }


    }

    public void CameraDown()
    {
        transform.position = Vector3.MoveTowards(transform.position, endSpot.position, speed * Time.deltaTime);
    }

     public void CameraUp()
    {
        transform.position = Vector3.MoveTowards(transform.position, topSpot.position, speed * Time.deltaTime);
    }
}
