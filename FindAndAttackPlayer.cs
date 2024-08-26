using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAndAttackPlayer : MonoBehaviour
{
    public float speed;
    public GameObject player;
    private float distToPlayer;
    private float distToSpot;
    public GameObject placeToGo;
    public Vector3 playerPos;
    public Vector3 newXZPos;
    public float heightY;
    public GameObject spot;
    public GameObject OGspot;
    public float distToLeave;

    public bool chasePlayer;
    public bool goToSpot;

    public GameObject flyingHM;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Instantiate(spot, player.transform.position, transform.rotation);
        Instantiate(OGspot, transform.position, transform.rotation);

        chasePlayer = true;
        goToSpot = false;
    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        distToSpot = Vector3.Distance(transform.position, spot.transform.position);

        //We chase the player around without chasing it in the air if they jump
        playerPos = player.transform.position;
        newXZPos = new Vector3 (playerPos.x, heightY, playerPos.z);

        //Makes the cat fly to the player
        if(chasePlayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, newXZPos, Time.deltaTime * speed);
        }

        //transform.position = Vector3.MoveTowards(transform.position, newXZPos, Time.deltaTime * speed);

        //Flies to spot after player.
        if(distToPlayer <= distToLeave)
        {
            chasePlayer = false;
            goToSpot = true;
        }

        if (goToSpot == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, spot.transform.position, Time.deltaTime * speed);
            
            if(this.gameObject.tag == "lvl2Cat2")
            {
                //If the cat misses the player, make the henchman follow the player and throw another cat (DestroyOnContact)
                flyingHM = GameObject.Find("Flying Henchman(Clone)/Physical Body");
                flyingHM.GetComponent<DropToFloor>().followUs = true;
                //flyingHM.GetComponent<HurtPlayer2R>().standNThrow = true;
            }

        }

        //If the players z is >= cats starting z, destroy cat

        if(player.transform.position.z <= OGspot.transform.position.z)
        {
            chasePlayer = false;
            goToSpot = true;
        }

        if(distToSpot <= 1)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
