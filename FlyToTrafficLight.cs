using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToTrafficLight : MonoBehaviour
{
    //This script allows the special cat to get flung to the traffic light and cause the cars to stop at the light


    public Transform trafficLight;
    public float speed;
    public CatAttackPlayer atkScript;

    public bool canFly = false;

    public GameObject[] carsA;
    GameObject allCars;
    public GameObject[] carsB;
    GameObject allCarsB;

    public bool lightReached;
    float distToLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distToLight = Vector3.Distance(transform.position, trafficLight.position);

        if (canFly == true)
        {
            JumpToTrafficLight();
        }


        //if player dies AND cat has hit traffic light, reset/teleport cat
        if(GameObject.Find("GameManager").GetComponent<HealthManager>().currentHealth <= 0)
        {
            //Fixes cat teleport on death glitch
            if(distToLight <= 1)
            {
                ResetKittyAndCars();
            }
        }
    }

    public void JumpToTrafficLight()
    {
        //Once canFly is set to true from the Enemy script, the cat will begin to fly.
        atkScript.enabled = false;
        transform.position = Vector3.MoveTowards(transform.position, trafficLight.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "crosslight")
        {
            //Once it trigger collides with the stop light, the cars either be deleted and be put back at the light. We used a list/array to place all the cars in and activate them all using a new variable. (allCars)
            for (int i = 0; i < carsA.Length; i++)
            {
                GameObject allCars = carsA[i];
                allCars.GetComponent<MakeCarGo>().stopLight = true;
            }

            for (int i = 0; i < carsB.Length; i++)
            {
                GameObject allCarsB = carsB[i];
                allCarsB.GetComponent<MakeCarGo2>().stopLight = true;
            }

            GetComponent<CatAttackPlayer>().spotScript.canDrop = true;
        }
    }

    //Resets the cat and cars
    public void ResetKittyAndCars()
    {
        canFly = false;
        atkScript.enabled = true;
        atkScript.ResetCat();
        for (int i = 0; i < carsA.Length; i++)
        {
            GameObject allCars = carsA[i];
            allCars.GetComponent<MakeCarGo>().stopLight = false;
        }

        for (int i = 0; i < carsB.Length; i++)
        {
            GameObject allCarsB = carsB[i];
            allCarsB.GetComponent<MakeCarGo2>().stopLight = false;
        }

    }
}
