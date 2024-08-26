using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrigger : MonoBehaviour
{
    public GameObject giantFrog;
    public GiantMove giantMoveScript;
    public GameObject target;

    public camSlideManager camSlideScript;

    //We're going to make it so that when you run into a cam trigger for the side,
    //the opposite cam trigger manager's slide bool becomes false, allowing the player
    //to respawn and go through a different path if they want.
    //public GameObject oppoCamSlideManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.tag == "giantTrigger")
        {
            giantFrog.SetActive(true);
            if(other.gameObject.tag == "Player")
            {
                giantMoveScript.startRolling = true;
            }
        }

        if(this.gameObject.tag == "camTrigger")
        {
            if(other.gameObject.tag == "Player")
            {
                //Move target down
                target = GameObject.Find("target");
                target.GetComponent<FollowPlayerZ>().goBro = true;
                target.GetComponent<FollowPlayerZ>().goGirl = false;
            }
        }

        if(this.gameObject.tag == "camTriggerUp")
        {
            if(other.gameObject.tag == "Player")
            {
                //Move target down
                target = GameObject.Find("target");
                target.GetComponent<FollowPlayerZ>().goGirl = true;
                target.GetComponent<FollowPlayerZ>().goBro = false;
            }
        }

        if(this.gameObject.tag == "camTriggerRight")
        {
            if(other.gameObject.tag == "Player")
            {
                //allow the target to move to the side
                target.GetComponent<FollowPlayerZ>().goGirl = false;

                //public GameObject oppoCamSlideManagerL = GameObject.Find("CamSlideLeftManager");

                //disable opposite manager's slide bool (in case you die and switch paths)
                GameObject.Find("CamSlideLeftManager").GetComponent<camSlideManager>().canSlide = false;

                camSlideScript.canSlide = true;
                camSlideScript.whichPoint++;

                GameObject.Find("GameManager").GetComponent<EnemyManager>().enemyList.Add(gameObject);
                gameObject.SetActive(false);
            }
        }

        if(this.gameObject.tag == "camTriggerLeft")
        {
            if(other.gameObject.tag == "Player")
            {
                //allow the target to move to the side
                target.GetComponent<FollowPlayerZ>().goGirl = false;

                //public GameObject oppoCamSlideManagerR = GameObject.Find("CamSlideRightManager");

                //disable opposite manager's slide bool (in case you die and switch paths)
                GameObject.Find("CamSlideRightManager").GetComponent<camSlideManager>().canSlide = false;
                
                camSlideScript.canSlide = true;
                camSlideScript.whichPoint++;

                GameObject.Find("GameManager").GetComponent<EnemyManager>().enemyList.Add(gameObject);
                gameObject.SetActive(false);

                
            }
        }

        if(this.gameObject.tag == "GTR")
        {
            if(other.gameObject.tag == "Player")
            {
                giantFrog.GetComponent<KeepMoving>().keepRollingR = true;
            }
        }

        if(this.gameObject.tag == "GTL")
        {
            if(other.gameObject.tag == "Player")
            {
                giantFrog.GetComponent<KeepMoving>().keepRollingL = true;
            }
        }
        
    }
}
