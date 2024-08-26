using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepMoving : MonoBehaviour
{
    public GameObject[] moreSpotsL;
    public GameObject[] moreSpotsR;
    public GiantMove moveScript;
    public bool keepRollingR;
    public bool keepRollingL;
    public int whichPoint;
    public float speed;
    float distToPatrolPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (keepRollingR == true)
        {
            moveScript.startRolling = false;
            transform.position = Vector3.MoveTowards(transform.position, moreSpotsR[whichPoint].transform.position, speed * Time.deltaTime);
            distToPatrolPoint = Vector3.Distance(transform.position, moreSpotsR[whichPoint].transform.position);
            if(distToPatrolPoint <= 0.02f)
            {
                whichPoint++;
            }
        }

        if (keepRollingL == true)
        {
            moveScript.startRolling = false;
            transform.position = Vector3.MoveTowards(transform.position, moreSpotsL[whichPoint].transform.position, speed * Time.deltaTime);
            distToPatrolPoint = Vector3.Distance(transform.position, moreSpotsL[whichPoint].transform.position);
            if(distToPatrolPoint <= 0.02f)
            {
                whichPoint++;
            }
        }

        if (whichPoint > 7)
        {
            keepRollingL = false;
            keepRollingR = false;
        }

        if(GameObject.Find("GameManager").GetComponent<EnemyManager>().nuts == true)
        {
            keepRollingL = false;
            keepRollingR = false;
            whichPoint = 0;
        }
    }
}
