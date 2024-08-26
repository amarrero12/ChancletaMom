using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBurglar : MonoBehaviour
{
    //public float backwardForce;
    public Rigidbody rb;
    public float speed;
    public GameObject player;
    public bool playerClimbed = false;

    public Vector3 playerPos;
    public Vector3 newXZPos;

    public Transform spot;

    public GameObject newSpot;
    public float distToPlayer;

    public bool goBack = false;



    // Start is called before the first frame update
    void Start()
    {
        //rb.AddForce(0, 0, -backwardForce);
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Vector3.Distance(transform.position, player.transform.position);

        //We chase the player around without chasing it in the air if they jump
        playerPos = player.transform.position;
        newXZPos = new Vector3 (playerPos.x, 11.075f, playerPos.z);

        if(playerClimbed == true){
            transform.position = Vector3.MoveTowards(transform.position, newXZPos, Time.deltaTime * speed);
            //rb.constraints = RigidbodyConstraints.FreezePositionY;
        }

        if(distToPlayer <= 3){
            playerClimbed = false;
            goBack = true;
        }

        if (goBack == true) {
            transform.position = Vector3.MoveTowards(transform.position, newSpot.transform.position, Time.deltaTime * speed);
        }

        //If the burglar reaches the edge of the building it will teleport back to its spot
        if(transform.position.z <= 228) {
            // playerClimbed = false;
            // goBack = false;
            // //transform.position = Vector3.MoveTowards(transform.position, spot.position, Time.deltaTime * speed);
            // transform.position = spot.position;
            ResetChargeBurglar();
        }
        
        //rb.AddForce(0, 0, -backwardForce);
    }

    public void ResetChargeBurglar()
    {
        //gameObject.SetActive(true);
        playerClimbed = false;
        goBack = false;
        transform.position = spot.position;
        rb.velocity = new Vector3(0f,0f,0f); 
        rb.angularVelocity = new Vector3(0f,0f,0f);
        transform.rotation = Quaternion.Euler(new Vector3(0f,0f,0f));
        // if(this.gameObject.tag == "chargeBurglar")
        // {
        //     rb.velocity = new Vector3(0f,0f,0f); 
        //     rb.angularVelocity = new Vector3(0f,0f,0f);
        //     transform.rotation = Quaternion.Euler(new Vector3(0f,0f,0f));
        // }
         
    }
}
