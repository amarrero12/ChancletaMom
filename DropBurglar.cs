using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBurglar : MonoBehaviour
{
    public BurglarJump burglarScript;

    //public GameObject theBurglar;

    //public bool burglarFell;

    void Update()
    {
        if(GameObject.Find("GameManager").GetComponent<HealthManager>().currentHealth <= 0)
        {
            GetComponent<Collider>().enabled = true;

        }
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Player"){
            //theBurglar.SetActive(true);
            burglarScript.JumpOff();
            burglarScript.PlayerCrossed = true;
            GetComponent<Collider>().enabled = false;
        }
    }
}
