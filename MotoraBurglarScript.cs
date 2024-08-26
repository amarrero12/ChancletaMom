using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotoraBurglarScript : MonoBehaviour
{
    public GameObject player;
    public GameObject whereToGo;
    public GameObject playerActivateMarker;

    private float distPlayer2Marker;

    public float speed;
    public bool goMotora = false;

    private float distToEnd;

    public GameObject startSpot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distPlayer2Marker = Vector3.Distance(player.transform.position, playerActivateMarker.transform.position);

        distToEnd = Vector3.Distance(transform.position, whereToGo.transform.position);

        if (distPlayer2Marker <= 2){
            goMotora = true;
        }

        if(goMotora == true) {
            transform.position = Vector3.MoveTowards(transform.position, whereToGo.transform.position, Time.deltaTime * speed);
        }

        if(distToEnd <= 2) {
            //Destroy(gameObject);
            GameObject.Find("GameManager").GetComponent<EnemyManager>().enemyList.Add(gameObject);
            ResetMC();
            gameObject.SetActive(false);
        }
    }

    public void ResetMC()
    {
        goMotora = false;
        transform.position = startSpot.transform.position;
    }
}
