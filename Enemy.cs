using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //maxHealth is public in case we want to change the amount of health a specific enemy starts with
    public int maxHealth = 1;
    int currentHealth;
    private float waitTime;
    private float startWaitTime;

    //Get access to the EnemyManager script in GameManager
    public GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        currentHealth = maxHealth;

        manager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this function is so that the enemy takes damage when attacked by the player
    public void TakeDamage (int damage)
    {
        //enemy health decreases depending on amount of damage dealt (usually 1?)
        currentHealth -= damage;

        //Play hurt animation (dont have one yet)

        //if enemy health reaches zero, run the Die function
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //EVERY OBJECT YOU WANT TO ATTACK MUST BE IN THE ENEMIES LAYER!!! NOT JUST TAGGED!


        // //For the charging burglar, if we hit it, it will fly backwards
        // if (gameObject.tag == "chargeBurglar")
        // {
        //     GetComponent<Rigidbody>().AddForce(0, 0, 750);
            
        // //Any other enemy will just be destroyed
        // } else {
        //     Debug.Log("Enemy died!");
        
        //     //Die animation (dont have one yet)

        //     //disable collider
        //     GetComponent<Collider>().enabled = false;

        //     //Disable the enemy
        //     //this.enabled = false;

        //     //or destroy it
        //     Destroy(this.gameObject);
        // }

        //For the charging burglar, if we hit it, it will fly backwards
        if (gameObject.tag == "chargeBurglar")
        {
            GetComponent<Rigidbody>().AddForce(0, 0, 300);
            GetComponent<ChargeBurglar>().playerClimbed = false;
            GetComponent<ChargeBurglar>().goBack = false;
            //transform.Translate(Vector3.forward * 300 * Time.deltaTime);
            //GameObject.Find("Player").GetComponent<PlayerAttack>().isAttacking = false;
        //Any other enemy will just be destroyed
        }

        else if (gameObject.tag == "kitty")
        {
            //We can get access to the kitty script and then the spot script and allow the "Player Location canDrop" bool to be true
            //so that the next cat can find it. Hooray!!!!
            Debug.Log("mondongo");
            GetComponent<Collider>().enabled = false;
            //GetComponent<Rigidbody>().AddForce(0, 0, 750);
            GetComponent<CatAttackPlayer>().spotScript.canDrop = true;
            // Destroy(this.gameObject);
            
            //Add the enemy to the list when we kill it so that it respawns if we die
            manager.GetComponent<EnemyManager>().enemyList.Add(gameObject);
            GetComponent<CatAttackPlayer>().ResetCat();
            gameObject.SetActive(false);
        } 

        else if (gameObject.tag == "specialkitty")
        {
            //GetComponent<Collider>().enabled = false;
            //GetComponent<CatAttackPlayer>().spotScript.canDrop = true;
            GetComponent<FlyToTrafficLight>().canFly = true;
        }

        else if (gameObject.tag == "lvl2Cat")
        {
            GetComponent<HitTheHM>().hitHenchman = true;
            GetComponent<FindAndAttackPlayer>().enabled = false;
        }

        else if (gameObject.tag == "lvl2Cat2")
        {
            GetComponent<HitTheHM>().hitHenchman = true;
            GetComponent<FindAndAttackPlayer>().enabled = false;
        }

        else if(gameObject.tag == "motoFling")
        {
            Debug.Log("boogabooga");
            GetComponent<HitTheTruck>().getFlung = true;
        }

        else if (gameObject.tag == "bounceable")
        {
            GetComponent<BreakBarrel>().Break();
            //Destroy(this.gameObject);
        }

        else if (gameObject.tag == "flyingHM")
        {
            Debug.Log("I'm hit!");
            //Destroy(this.gameObject);
            GetComponent<HitTheTruck>().getFlung = true;
        }
        
        else {
            Debug.Log("Enemy died!");
        
            //Die animation (dont have one yet)

            //disable collider
            //GetComponent<Collider>().enabled = false;

            //Disable the enemy
            //this.enabled = false;

            //or destroy it
            //Destroy(this.gameObject);

            manager.GetComponent<EnemyManager>().enemyList.Add(gameObject);
            gameObject.SetActive(false);
        }

        
    }
}
