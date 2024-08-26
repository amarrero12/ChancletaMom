using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTheTruck : MonoBehaviour
{
    public GameObject truck;
    public float flingSpeed;
    public bool getFlung;

    // Start is called before the first frame update
    void Start()
    {
        truck = GameObject.Find("Truck");
    }

    // Update is called once per frame
    void Update()
    {
        if(getFlung == true)
        {
            FlingToTruck();
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if(this.gameObject.tag != "flyingHM")
        {
            if(other.gameObject.tag == "truckboss")
            {
                //Add damage to the truck every time a HM moto hits it.
                //Destroy(other.gameObject);
                truck.GetComponent<TruckManager>().truckDamage++;
                Destroy(this.gameObject);
                truck.GetComponent<DropBombs>().dropDaBarrel = true;
            }
        }
        else{
            if(other.gameObject.tag == "truckboss")
            {
                truck.GetComponent<TruckManager>().truckDamage++;
                Destroy(this.gameObject);
            }

        }
        
    }

    public void FlingToTruck()
    {
        GetComponent<PushMoto>().drive = false;
        transform.position = Vector3.MoveTowards(transform.position, truck.transform.position, flingSpeed * Time.deltaTime);
    }
}
