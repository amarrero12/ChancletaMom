using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushMoto : MonoBehaviour
{
    public bool drive;
    public GameObject destination;
    public float speed;
    public float distToDest;
    public GameObject truck;
    public HurtPlayer2R hurtScript;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distToDest = Vector3.Distance(transform.position, destination.transform.position);

        if (drive == true)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, Time.deltaTime * speed);

            if(this.gameObject.tag == "flyingHM")
            {
                if(distToDest <= 1)
                {
                    //drive = false;
                    hurtScript.standNThrow = true;
                    Destroy(destination.gameObject);
                    drive = false;
                    //So that its not constantly keeping standNThrow true
                }
            }
        }

        if(this.gameObject.tag == "tripHM")
        {
            if(distToDest <= 3)
            {
                transform.rotation = Quaternion.Euler(new Vector3(270, 0, 0));
            }
        }

        if (this.gameObject.tag == "motoFling")
        {
            if(distToDest <= 1)
            {
                Debug.Log("aha!");

                truck = GameObject.Find("Truck");
                if(truck.GetComponent<DropBombs>().barrelCounter != 21 || truck.GetComponent<DropBombs>().barrelCounter != 22)
                {
                    truck.GetComponent<DropBombs>().dropDaBarrel = true;
                }
                //truck.GetComponent<DropBombs>().dropDaBarrel = true;
                Destroy(this.gameObject);
            }
        }

        
    }
}
