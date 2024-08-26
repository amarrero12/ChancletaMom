using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    public float speed;
    public GameObject[] patrolPoints;
    int whichPoint;
    public float distToPatrolPoint;

    public float startWaitTime;
    private float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        whichPoint = 0;
        waitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[whichPoint].transform.position, Time.deltaTime * speed);

        distToPatrolPoint = Vector3.Distance(transform.position, patrolPoints[whichPoint].transform.position);

        if(this.gameObject.tag == "turtle")
        {
            if (distToPatrolPoint < 0.02f)
            {
                if(waitTime <= 0)
                {
                    if(whichPoint != 1)
                    {
                        whichPoint++;
                    }
                    else
                    {
                        whichPoint = 0;
                    }
                    waitTime = startWaitTime;
                }
                else{
                    waitTime -= Time.deltaTime;
                }
            }
        } else {
            if (distToPatrolPoint < 0.02f)
            {
                if(whichPoint != 1)
                {
                    whichPoint++;
                }

                else
                {
                   whichPoint = 0;
                }
            }
        }
        
    }

    public void Pause()
    {
        
    }
}
