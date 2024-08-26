using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAttackPlayer : MonoBehaviour
{
    //This entire script allows for the cat to jump at a spot the player instantiates, when it is near the cat. Once the cat is close
    //enough to the spot, it will return to its home spot. Once it reaches the home spot, the initial player spot is destroyed. Our bools
    //are then reset so if we are near the cat again, another spot will spawn from the DropObjectLocation script. Notice how the entire
    //process relies on the boolean and not exactly the player's distance. This is because if we rely only on the player's distance, the
    //cat might freeze in midair if the player moves far away. The player only triggers the boolean into being true, which then allows
    //the function to run fully (the cat to jump to the spot and go back home), and then makes the boolean false in the end, 
    // stopping it completely so that the cat can finish moving even if the player has left already. 



    public float speed;
    float distToPlayer;
    float distToSpot;
    float distToHome;

    public bool canJumpAtPlayer;
    //private float jumpedTime;
    //public float startJumpedTime;

    public GameObject player;
    //public GameObject cylinderSpot;
    //public Transform playerSpot;

    public DropObjectLocation spotScript;

    public GameObject catSpot;

    //public bool catIsAtRest;

    // public GameObject[] patrolPoints;
    // int whichPoint;
    // float distToPatrolPoint;

    public bool catIsAtRest;


    // Start is called before the first frame update
    void Start()
    {
        canJumpAtPlayer = false;

        //jumpedTime = startJumpedTime;

        //Instantiate(catSpot, transform.position, transform.rotation);

        catIsAtRest = true;

    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distToPlayer <= 7)
        {
            canJumpAtPlayer = true; 
            //JumpAtPlayer();

            // jumpedTime -= Time.deltaTime;
            // if (Time.deltaTime <= 0)
            // {
            //     canJumpAtPlayer = false;
            //     jumpedTime = startJumpedTime;
            // }
            
            // jumpedTime = startJumpedTime;
            // jumpedTime -= Time.deltaTime;

        }
        JumpAtPlayer();
    }

    void JumpAtPlayer()
    {
        if (canJumpAtPlayer == true)
        {
            // catIsAtRest = false;
            // Debug.Log("mondong");
            // catIsAtRest = true

            spotScript.DropLocation();
            GameObject emptySpot = GameObject.Find("EmptySpot(Clone)");
            if (catIsAtRest == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, emptySpot.transform.position, Time.deltaTime * speed);
                //catIsAtRest = false;
            }

            distToSpot = Vector3.Distance(transform.position, emptySpot.transform.position);

            if (distToSpot <= 0.1)
            {
                //catSpot = GameObject.Find("CatSpot(Clone)");
                catIsAtRest = false; 
                Debug.Log("dfggh");
                //transform.position = Vector3.MoveTowards(transform.position, catSpot.transform.position, Time.deltaTime * speed);
            }

            if (catIsAtRest == false)
            {
                //catSpot = GameObject.Find("CatSpot(Clone)");
                transform.position = Vector3.MoveTowards(transform.position, catSpot.transform.position, Time.deltaTime * speed); 
            }

            distToHome = Vector3.Distance(transform.position, catSpot.transform.position);
            if (distToHome <= 0.1)
            {
                //destroys the clone not the prefab! Don't destroy prefabs!
                Destroy(emptySpot.gameObject);
                catIsAtRest = true;
                spotScript.canDrop = true;
                canJumpAtPlayer = false;
            }
        }

    }

    public void ResetCat()
    {
        //This function will completely reset the cat so there are no problems when it respawns. It gets called by the enemy script when attacked
        transform.position = catSpot.transform.position;
        canJumpAtPlayer = false;
        catIsAtRest = true;
        spotScript.canDrop = true;
        
    }
}
