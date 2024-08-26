using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer2R : MonoBehaviour
{
    //This is a script for hurting the player in level 2. Since the player is auto running, if it bumps into
    //anything hazardous, the player will take damage but keep on.

    //The point of the poof boolean is whether or not i want a hazard or enemy to disappear after bumping into it.
    public bool poof;

    public bool lePoof;

    public int damageToGive = 1;
    public GameObject cat;
    public bool standNThrow;
    
    private void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.tag == "rbstanding")
        {
            if(other.gameObject.tag == "Player")
            {
                Vector3 hitDirection = other.transform.position - other.transform.position;
                FindObjectOfType<HealthManager>().HurtPlayer(damageToGive, hitDirection);

                //Level 2 instantiates prefabs so they can be destroyed
                if (poof == true)
                {
                    Destroy(this.gameObject);
                }

                if(lePoof == true)
                {
                    GameObject.Find("GameManager").GetComponent<EnemyManager>().barrels.Add(gameObject);
                    gameObject.SetActive(false);
                }
                
            }
        }
        else
        {
            //If touches player, player takes damage
            if(other.gameObject.tag == "Player")
            {
                //creates a vector3 out of the players position and the cactus' position (as it came and got hit)
                Vector3 hitDirection = other.transform.position - transform.position;
                //normalized restricts it to being a straight line distance
                hitDirection = hitDirection.normalized;

                //the healthmanager is fed the damage we will take and which way to knock us back (player - hazard positions)
                FindObjectOfType<HealthManager>().HurtPlayer(damageToGive, hitDirection);

                if (poof == true)
                {
                    if (gameObject.tag == "lvl2Cat")
                    {
                        GetComponent<ResetLocation>().ResetMe();
                    }
                    //Destroy(this.gameObject);
                    gameObject.SetActive(false);
                    
                }
                
            }
        }

        // if(other.gameObject.tag == "Player")
        // {
        //     //creates a vector3 out of the players position and the cactus' position (as it came and got hit)
        //     Vector3 hitDirection = other.transform.position - transform.position;
        //     //normalized restricts it to being a straight line distance
        //     hitDirection = hitDirection.normalized;

        //     //the healthmanager is fed the damage we will take and which way to knock us back (player - hazard positions)
        //     FindObjectOfType<HealthManager>().HurtPlayer(damageToGive, hitDirection);

        //     if (poof == true)
        //     {
        //         Destroy(this.gameObject);
        //     }
            
        // }

        //When the player hits the cat and the cat hits the HM, HM dies (set false)
        if(other.gameObject.tag == "lvl2Cat")
        {
            GameObject.Find("GameManager").GetComponent<EnemyManager>().enemyList.Add(gameObject);
            cat.GetComponent<HitTheHM>().hitHenchman = false;
            gameObject.SetActive(false);
        }

        

        // if(other.gameObject.tag == "placeToThrowFromLvl2")
        // {
        //     Instantiate(cat, other.gameObject.transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
        // }
    }

    void OnTriggerStay(Collider other)
    {
        if(this.gameObject.tag == "toxic sludge")
        {
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

    void Update()
    {
        if(standNThrow == true)
        {
            if(this.gameObject.tag == "flyingHM")
            {
                Instantiate(cat, new Vector3(transform.position.x, transform.position.y - 2, transform.position.z - 5), Quaternion.Euler(new Vector3(0, 180, 0)));
                //Insert parenting code here
                standNThrow = false;
                cat.SetActive(true);
            }
            else if(gameObject.tag == "sleepy2")
            {
                //Instantiate(cat, new Vector3(transform.position.x, transform.position.y, transform.position.z - 2), Quaternion.Euler(new Vector3(0, 180, 0)));
                standNThrow = false;
                cat.SetActive(true);
                cat.GetComponent<FindAndAttackPlayer>().enabled = true;
            }

            else
            {
                Instantiate(cat, transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                standNThrow = false;
                cat.SetActive(true);
            }
            // Instantiate(cat, transform.position, Quaternion.Euler(new Vector3(0, 180, 0)));
            // standNThrow = false;
            // cat.SetActive(true);
        }

    }


    //This next section is for throwing items at the player
    //maybe use a bool that only turns true once the player touches the trigger? or the moto reaches the middle?
    //scrap the bool and use a function? on trigger enter?
    //if bool is true, if moto is at middle or player touches trigger, instantiate object
    //in object script, go to players dropped location
}
