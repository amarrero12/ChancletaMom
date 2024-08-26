using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBombs : MonoBehaviour
{
    public GameObject[] dropSpots;
    GameObject currentSpot;
    int whichSpot;
    public GameObject redBarrel;
    public bool dropDaBarrel;

    public float startDropTime;
    public float dropTime;
    public float startDropTime2;

    public float barrelCounter;

    public GameObject motoHM;
    public GameObject opoMotoHM;
    public GameObject BMotoHM;
    public bool HMDrivesIn;
    public bool opoHMDrivesIn;
    public bool BMotoHMDrivesIn;

    public TruckManager managerScript;


    // Start is called before the first frame update
    void Start()
    {
        dropTime = startDropTime;
    }

    // Update is called once per frame
    void Update()
    { 
        //Debug.Log(barrelCounter);
        if (dropDaBarrel == true)
        {
            ThrowABarrel();
            //dropDaBarrel = false;
        }

        if(managerScript.firstPhase == true)
        {
            //If we drop 10 barrels, stop dropping them, instantiate the henchman moto and set the counter back to 0 to avoid endless instatiates.
            if (barrelCounter >= 10)
            {
                dropDaBarrel = false;
                HMDrivesIn = true;
                barrelCounter = 0;
            }
        }

        if(managerScript.secondPhase == true)
        {
            //If we drop 10 barrels, stop dropping them, instantiate the henchman moto and set the counter back to 0 to avoid endless instatiates.
            if (barrelCounter >= 20)
            {
                dropDaBarrel = false;
                opoHMDrivesIn = true;
                barrelCounter = 0;
            }
        }

        if (managerScript.thirdPhase == true)
        {
            if (barrelCounter == 5)
            {
                dropDaBarrel = false;
                BMotoHMDrivesIn = true;
                barrelCounter = 6;
            }

            if (barrelCounter == 12)
            {
                dropDaBarrel = false;
                BMotoHMDrivesIn = true;
                barrelCounter = 13;
            }

            if (barrelCounter == 20)
            {
                dropDaBarrel = false;
                BMotoHMDrivesIn = true;
                //We set the counter to 21 so that other scripts don't let it keep dropping barrels
                barrelCounter = 21;
            }

            if (barrelCounter == 21)
            {
                managerScript.spawnFlyer = true;
                barrelCounter = 22;
            }

            if(barrelCounter == 22)
            {
                dropDaBarrel = false;
            }
        }
        

        if (HMDrivesIn == true)
        {
            dropDaBarrel = false;
            Instantiate(motoHM, new Vector3 (18.6f, 0.58f, transform.position.z + 27), Quaternion.Euler(new Vector3(0, 90, 0)));
            HMDrivesIn = false;
        }

        if (opoHMDrivesIn == true)
        {
            dropDaBarrel = false;
            Instantiate(opoMotoHM, new Vector3 (18.6f, 0.58f, transform.position.z + 27), Quaternion.Euler(new Vector3(0, 90, 0)));
            opoHMDrivesIn = false;
        }

        if (BMotoHMDrivesIn == true)
        {
            dropDaBarrel = false;
            Instantiate(BMotoHM, new Vector3 (18.6f, 0.58f, transform.position.z + 27), Quaternion.Euler(new Vector3(0, 90, 0)));
            BMotoHMDrivesIn = false;
        }

        if(Input.GetKeyDown("z"))
        {
            barrelCounter = 19;
        }

    }

    //WE'RE DOING THIS IN THE TRUCK MANAGER NOW SO THAT ALL THE PHASE BOOLS CAN BE THERE
    // //When we hit the bombtrigger, start dropping bombs
    // private void OnTriggerEnter (Collider other)
    // {
    //     if (other.gameObject.tag == "bombtrigger"){
    //         dropDaBarrel = true;
    //     }
    // }

    //Drops a random bomb using time
    public void ThrowABarrel()
    {
        if (dropTime <= 0)
        {
            whichSpot = Random.Range (0, dropSpots.Length);
            currentSpot = dropSpots[whichSpot];
            Instantiate(redBarrel, currentSpot.transform.position, Quaternion.Euler(new Vector3(0, 270, 0)));
            
            //The counter helps us count how many bombs were dropped so that we can make something happen
            barrelCounter++;
            dropTime = startDropTime;
        }
        else
        {
            dropTime -= Time.deltaTime;
        }
    }


    //
    // public void ActivateMoto()
    // {
    //     Instantiate
    // }
}
