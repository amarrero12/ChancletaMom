using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathFall : MonoBehaviour
{
    public HealthManager healthScript;

    //int damageToGive = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized;
            FindObjectOfType<HealthManager>().HurtPlayer(2, hitDirection);
        }
    }
}
