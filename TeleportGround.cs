using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGround : MonoBehaviour
{
    // public GameObject player;
    // public Transform groundTransform;

    public Transform startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("GameManager").GetComponent<EnemyManager>().nuts == true)
        {
            transform.position = startingPosition.position;
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.position = transform.position + new Vector3(0, 0, 397.35f);
        }
    }
}
