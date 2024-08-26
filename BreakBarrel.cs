using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBarrel : MonoBehaviour
{

    public GameObject platano;
    public GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
        //transform.rotation = Quaternion.Euler(new Vector3(0, -90, 90));
        //Gotta use find because the barrel is a prefab
        manager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Break()
    {
        //transform.rotation = Quaternion.Euler(new Vector3(0, -90, 90));
        Instantiate(platano, transform.position, Quaternion.Euler(new Vector3(0, -90, 90)));
        //Destroy(gameObject);

        //Add the barrel to the list when we break it so that it respawns if we die
        manager.GetComponent<EnemyManager>().barrels.Add(gameObject);

        //Set it inactive instead of destroying it so we can respawn it later
        gameObject.SetActive(false);
    }
}
