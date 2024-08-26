using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantMove : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public bool startRolling;
    public GameObject endspot;
    public GameObject startSpot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startRolling == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, endspot.transform.position, Time.deltaTime * speed);
        }

        //rb.AddForce(0, 0, Time.deltaTime * -speed);
        //MAybe use location points and moveTowards? Using AddForce is making him constantly go faster.

        if(GameObject.Find("GameManager").GetComponent<EnemyManager>().nuts == true)
        {
            transform.position = startSpot.transform.position;
            startRolling = false;
        }
    }
}
