using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObjectLocation : MonoBehaviour
{
    //this is the object that will drop for when we enter the range for the cat to chase us(the spot(prefab))
    public GameObject emptySpot;

    //this is the location being put in instantiate so we can subtract from the Y so that its on the floor
    public Vector3 theLocation;

    public bool canDrop;

    // Start is called before the first frame update
    void Start()
    {
        canDrop = true;
    }

    // Update is called once per frame
    void Update()
    {
        //subtract 1 from the Y so that it will be on the floor
        theLocation = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        //if I press V, the spot will be instantiated
        // if (Input.GetKeyDown(KeyCode.V))
        // {
        //     Debug.Log("V");
        //     Instantiate(emptySpot, theLocation, transform.rotation);
        // }
    }

    public void DropLocation ()
    {
        if (canDrop == true)
        {
            Debug.Log("V");
            Instantiate(emptySpot, theLocation, transform.rotation);
            canDrop = false;
        }
        
    }
}
