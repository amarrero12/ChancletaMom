using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camSlideManager : MonoBehaviour
{
    public float speed;
    public GameObject[] patrolPoints;
    public int whichPoint;
    public GameObject target;

    public bool canSlide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canSlide == true)
        {
            target.transform.position = Vector3.MoveTowards(target.transform.position, new Vector3(patrolPoints[whichPoint].transform.position.x, target.transform.position.y, target.transform.position.z), speed * Time.deltaTime);
        }

        if(GameObject.Find("GameManager").GetComponent<EnemyManager>().nuts == true)
        {
            whichPoint = 0;
            // canSlide = false;
            // target.transform.position = target.GetComponent<FollowPlayerZ>().topSpot.transform.position;
            
            // if(target.GetComponent<FollowPlayerZ>().goBro == true)
            // {
            //     target.transform.position = patrolPoints[whichPoint].transform.position;
            // }
        }
    }
}
