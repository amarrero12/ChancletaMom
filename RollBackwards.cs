using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBackwards : MonoBehaviour
{
    public float moveBackSpeed;
    public float moveBackSpeed2;
    public Vector3 moveDirection;
    public bool canBounce;
    public Rigidbody rb;
    public GameObject truck;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        truck = GameObject.Find("Truck");
        
        if (truck.GetComponent<TruckManager>().secondPhase == true)
        {
            canBounce = true;
        }

        if (truck.GetComponent<TruckManager>().thirdPhase == true)
        {
            canBounce = true;
        }

        transform.Translate(Vector3.forward * Time.deltaTime * - moveBackSpeed, Space.World);

        if (canBounce == true)
        {
            moveBackSpeed = moveBackSpeed2;
        }
    }

    public void Bounce()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (canBounce == true)
        {
            rb.AddForce(0, 400, 0);
        }

        //Debug.Log("collision");
    }

    // private void OnCollisionStay(Collision other)
    // {
    //     if (canBounce == true)
    //     {
    //         rb.AddForce(0, 400, 0);
    //     }
    // }
}
