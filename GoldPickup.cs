using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickup : MonoBehaviour
{
    public int value;

    public GameObject pickupEffect;

    public GameObject manager;

    //public EnemyManager managerScript;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When the player comes in contact with a platano, the script will search for the game manager object, and do the add gold function,
    // adding whatever number our value is, and then destroy the gold object
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameManager>().AddGold(value);

            //create and run the particle effect at the same pos and rot of the gold bar
            Instantiate(pickupEffect, transform.position, transform.rotation);

            //if this platano is NOT a barrel platano, add it to the list
            //We only want normal field platanos respawning, not barrel ones
            if(this.gameObject.tag != "barrelplatano")
            {
                //add to platano list so that they respawn if we die
                manager.GetComponent<EnemyManager>().platanos.Add(gameObject);
                gameObject.SetActive(false);
            }
            else{
                //Destroy the barrelplatanos after collecting them because we will not respawn those
                Destroy(gameObject);
            }

            

        }
    }
}
