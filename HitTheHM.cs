using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTheHM : MonoBehaviour
{
    public Transform MyHenchman;
    public float speed;
    public CatAttackPlayer atkScript;

    public bool hitHenchman = false;

    //public GameObject theHenchman;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hitHenchman == true)
        {
            if(this.gameObject.tag == "lvl2Cat2")
            {
                MyHenchman = GameObject.Find("Flying Henchman(Clone)/Physical Body").transform;
            }
            //atkScript.enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, MyHenchman.position, speed * 2 * Time.deltaTime);
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if(other.gameObject.tag == "lvl2Cat")
    //     {
    //         Debug.Log("mondongo");
    //         theHenchman.GetComponent<HurtPlayer2R>().catkill = true;
    //     }
    // }
}
