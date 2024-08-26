using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public HealthManager theHealthMan;

    public Renderer theRend;

    public Material cpOff;
    public Material cpOn;

    //This bool will be used to allow the player to save only when they first hit the checkpoint. If they try to progress and go back to this checkpoint,
    //to save their future progress, it will not resave. (Everything from after the CP will still respawn if the player dies)
    public bool canSave = true;


    // Start is called before the first frame update
    void Start()
    {
        //find healthmanager so we don't have to manually do it in unity every time
        theHealthMan = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckpointOn()
    {
        //an array to create a list of checkpoints and equates it to find any objects in the world with the checkpoint script attached to put them in the list
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();

        //for every checkpoint item that we're now calling cp in our checkpoints array
        foreach (Checkpoint cp in checkpoints)
        {
            //change the material of every checkpoint in the list off. Will not conflict with this current checkpoint because the next line ensures that it stays on.
            cp.CheckpointOff();
        }

        //switches the material to the "checkpoint on" one
        theRend.material = cpOn;
    }

    public void CheckpointOff()
    {
        //switches the material to the "checkpoint off" one
        theRend.material = cpOff;
    }

    private void OnTriggerEnter(Collider other)
    {
        //same as "other.tag == ("Player")
        if(other.tag.Equals("Player"))
        {
            //sets spawn point to the current position of this checkpoint object
            theHealthMan.SetSpawnPoint(transform.position);
            //call the checkpoint on function that will change the color material
            CheckpointOn();
            
            if (canSave == true)
            {
                //Clears the lists when the player arrives at a CP. (Items/enemies won't respawn)
                GameObject.Find("GameManager").GetComponent<EnemyManager>().nuts = true;
                //Disables player's ability to resave from the same CP twice
                canSave = false;
            }
        }
    }
}
