using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    public bool hitOther;
    public GameObject flyingHM;
    public GameObject manager;

    private void OnTriggerEnter(Collider other)
    {
        manager = GameObject.Find("GameManager");
        // if(other.gameObject.tag == "Player")
        // {
        //     GetComponent<Rigidbody>().AddForce(0, 0, -750);
        //     //Destroy(gameObject);
        // }

        //If this burglar hits another, that one will destroy and this one will fly backwards some more so it doesnt get stopped by the player.
        if (other.gameObject.tag == "burglartag")
        {
            manager.GetComponent<EnemyManager>().enemyList.Add(other.gameObject);
            other.gameObject.GetComponent<ChargeBurglar>().ResetChargeBurglar();
            other.gameObject.SetActive(false);
            GetComponent<Rigidbody>().AddForce(0, 0, 350);
        }

        //If the burglar reaches an invisible wall, it will be destroyed
        if (other.gameObject.tag == "BurglarKillWall"){
            manager.GetComponent<EnemyManager>().enemyList.Add(gameObject);
            GetComponent<ChargeBurglar>().ResetChargeBurglar();
            gameObject.SetActive(false);
        }



        //FOR LEVEL 2 ONLY:
        if (other.gameObject.tag == "redbarrel2" || other.gameObject.tag == "rbstanding")
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "lvl2Cat")
        {
            other.gameObject.GetComponent<ResetLocation>().ResetMe();
            other.gameObject.SetActive(false);
        }

        if(other.gameObject.tag == "lvl2Cat2")
        {
            // We're doing FollowUs in the FindAndAttackPlayer script now

            //Throw another cat
            flyingHM = GameObject.Find("Flying Henchman(Clone)/Physical Body");
            flyingHM.GetComponent<HurtPlayer2R>().standNThrow = true;
            //flyingHM.GetComponent<HurtPlayer2R>().standNThrow = false;
            Debug.Log("booty");

            // flyingHM.GetComponent<DropToFloor>().followUs = true;
            Destroy(other.gameObject);

        }
    }

    // private void Wait (){
    //     burglarScript.speed = 0;
    //     waitTimeLeft -= Time.deltaTime;

    //     if (waitTimeLeft <= 0){
    //         burglarScript.speed = 10;
    //         waitTimeLeft = startWaitTime;
    //         iHit = false;
    //     }
    // }
}
