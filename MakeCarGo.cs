using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCarGo : MonoBehaviour
{
    public float driveToX;
    public Transform spawnLocation;
    public Transform originalPosition;

    public float speed;

    public bool stopLight;

    // Start is called before the first frame update
    void Start()
    {
        stopLight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopLight == false)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        if(transform.position.x >= driveToX)
        {
            transform.position = (spawnLocation.position);
        }

        if (stopLight == true)
        {
            if (gameObject.tag == "maincar")
            {
                transform.position = (spawnLocation.position);
            }

            else{
                GameObject.Find("GameManager").GetComponent<EnemyManager>().enemyList.Add(gameObject);
                transform.position = originalPosition.position;
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }

        if(GameObject.Find("GameManager").GetComponent<EnemyManager>().nuts == true)
        {
            transform.position = originalPosition.position;
            stopLight = false;
        }
    }
}
