using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBack : MonoBehaviour
{
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlyBackwards() 
    {
        rb.AddForce(0, 0, 1000);
    }
}
