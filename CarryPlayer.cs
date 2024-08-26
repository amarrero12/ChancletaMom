using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("mondongo");
            col.gameObject.transform.SetParent(gameObject.transform,true);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.transform.parent = null;
        }
    }
}
