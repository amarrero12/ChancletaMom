using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurglarJump : MonoBehaviour
{
    public bool PlayerCrossed = false;
    public Rigidbody rb;
    public float dropForce = 50f;
    public int damageToGive = 1;

    public Vector3 startingSpot;

    public bool canCross;

    public GameObject manager;

    void Start()
    {
        startingSpot = transform.position;

        manager = GameObject.Find("GameManager");
    }

    void Update()
    {
        if (PlayerCrossed == true)
        {
            // //Add to list when player crosses
            // manager.GetComponent<EnemyManager>().enemyList.Add(gameObject);

            if(manager.GetComponent<HealthManager>().currentHealth <= 0)
            {
                PlayerCrossed = false;
                ResetFallBurglar();
            }
        }
        
    }

    public void JumpOff () {
        //Add to list when jumpoff
        manager.GetComponent<EnemyManager>().enemyList.Add(gameObject);

        //Turns off constraints in the editor
        rb.constraints = RigidbodyConstraints.None;
        rb.AddForce(0, -dropForce, 0);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {

            //Hits the player in a certain direction
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized;
            FindObjectOfType<HealthManager>().HurtPlayer(damageToGive, hitDirection);


            ResetFallBurglar();
            gameObject.SetActive(false);
        }
    }

    public void ResetFallBurglar()
    {
        transform.position = startingSpot;
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        PlayerCrossed = false;
    }

}
