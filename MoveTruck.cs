using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTruck : MonoBehaviour
{
    public bool pushTruckToPoints = false;
    public bool goForward = false;
    public float speed;
    public GameObject[] patrolPoints;
    int whichPoint;
    public float distToPatrolPoint;

    public Transform playerTransform;

    Vector3 pos;

    public float howFar;
    public float howFar2;

    public DropBombs bombScript;

    public Transform startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        whichPoint = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pushTruckToPoints == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[whichPoint].transform.position, Time.deltaTime * speed);

            distToPatrolPoint = Vector3.Distance(transform.position, patrolPoints[whichPoint].transform.position);

            if (distToPatrolPoint < 0.02f)
            {
                if(whichPoint != 2)
                {
                    whichPoint++;
                }

                else
                {
                    pushTruckToPoints = false;
                    goForward = true;
                }
            }
        }

        if(whichPoint == 1)
        {
            //transform.eulerAngles.y = 45f;
            transform.rotation = Quaternion.Euler(new Vector3(0, 45, 0));
        }

        if(whichPoint == 2)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }


        //For moving the truck forward after hitting the last patrol point (actually its following the player's z coodinate plus how far we want it from the player)
        if (goForward == true)
        {
            pos = new Vector3(transform.position.x, transform.position.y, playerTransform.position.z + howFar);
            transform.position = pos;
            //bombScript.dropDaBarrel = true;
        }

        //Completely reset truck boss fight when you die
        if(GameObject.Find("GameManager").GetComponent<EnemyManager>().nuts == true)
        {
            whichPoint = 0;
            transform.position = startingPosition.position;
            transform.rotation = startingPosition.rotation;
            pushTruckToPoints = false;
            goForward = false;
            bombScript.dropDaBarrel = false;
            bombScript.barrelCounter = 0;
            GetComponent<TruckManager>().truckDamage = 0;
            GetComponent<TruckManager>().spawnFlyer = false;
            bombScript.HMDrivesIn = false;
            bombScript.opoHMDrivesIn = false;
            bombScript.BMotoHMDrivesIn = false;
            GetComponent<TruckManager>().firstPhase = false;
            GetComponent<TruckManager>().secondPhase = false;
            GetComponent<TruckManager>().thirdPhase = false;
            bombScript.startDropTime = 1;
            bombScript.dropTime = bombScript.startDropTime;
            howFar = 35;
            Destroy(GameObject.Find("Red Barrel(Clone)"));
            Destroy(GameObject.Find("Moto Henchman Crossing(Clone)"));
            Destroy(GameObject.Find("Moto Henchman Crossing Opposite(Clone)"));
            Destroy(GameObject.Find("Moto Henchman Crossing FAST(Clone)"));
            Destroy(GameObject.Find("Flying Henchman(Clone)"));

        }
        

    }
}
