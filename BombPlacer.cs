using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPlacer : MonoBehaviour
{
    public GameObject standBarrel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "dropLocations")
        {
            Instantiate(standBarrel, other.transform.position, Quaternion.Euler(new Vector3(270, 0, 0)));
        }
    }


}
