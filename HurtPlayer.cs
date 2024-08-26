using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    //write it this way so that if we forget to write a number in the editor, the default is 1
    //and it will hit
    public int damageToGive = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the player runs into it, use the Health Manager script to run the HurtPlayer function
        //and give damage based on damageToGive value

        //the player will get hit, the cactus will recognize what direction it came from and give it to the healthmanager
        if(other.gameObject.tag == "Player")
        {
            //creates a vector3 out of the players position and the cactus' position (as it came and got hit)
            Vector3 hitDirection = other.transform.position - transform.position;
            //normalized restricts it to being a straight line distance
            hitDirection = hitDirection.normalized;

            //the healthmanager is fed the damage we will take and which way to knock us back (player - hazard positions)
            FindObjectOfType<HealthManager>().HurtPlayer(damageToGive, hitDirection);
        }
    }
}
