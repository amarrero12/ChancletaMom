using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpotDestroyer : MonoBehaviour
{
    public GameObject player;
    private float distToPlayer;
    public float howClose;

    private GameObject catspots;
    private GameObject cats;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distToPlayer <= howClose)
        {
            catspots = GameObject.Find("CatSpot(Clone)");
            cats = GameObject.Find("cat(Clone)");
            Destroy(catspots);
            Destroy(cats);
            //Destroy(this.gameObject);
        }
    }
}
