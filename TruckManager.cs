using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    public float truckDamage = 0;
    public bool firstPhase;
    public bool secondPhase;
    public bool thirdPhase;
    //public GameObject barrels;
    public DropBombs bombDropper;
    public MoveTruck moveScript;

    public GameObject flyingHM;
    public bool spawnFlyer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //barrels = GameObject.Find("Red Barrel(Clone)");

        //Debug.Log(truckDamage);

        if(truckDamage == 1)
        {
            firstPhase = false;
            secondPhase = true;
        }

        if(secondPhase == true)
        {
            //bombDropper.dropDaBarrel = true;
            //barrels.GetComponent<RollBackwards>().canBounce = true;
            moveScript.howFar = moveScript.howFar2;
            bombDropper.startDropTime = bombDropper.startDropTime2;
        }

        if (truckDamage == 2)
        {
            secondPhase = false;
            thirdPhase = true;
        }

        if (truckDamage == 3)
        {
            Debug.Log("YOU WIN!");
            Destroy(this.gameObject);
            GameObject.Find("SceneManager").GetComponent<switchScene>().LoadNextScene();
        }

        if (thirdPhase == true)
        {
            
        }

        if(spawnFlyer == true)
        {
            Instantiate(flyingHM, new Vector3(transform.position.x - 9, 0, transform.position.z + 200), transform.rotation);
            spawnFlyer = false;
        }
        

    }

    //When we hit the bombtrigger, start dropping bombs
    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "bombtrigger"){
            firstPhase = true;
            bombDropper.dropDaBarrel = true;
        }
    }
}
