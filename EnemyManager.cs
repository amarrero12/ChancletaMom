using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //This script will help us count and respawn enemies when we die, and also keep them dead when we reach checkpoints
    //We will also do the same for platanos and barrels

    //public GameObject[] enemies;
    //public GameObject[] platanos;

    //We use a List that we can add the platanos into every time we collect one
    public List<GameObject> platanos = new List<GameObject>();

    //This bool is for removing the platanos from the List so it doesn't overflow
    public bool nuts;

    public List<GameObject> barrels = new List<GameObject>();

    public List<GameObject> enemyList = new List<GameObject>();

    //items that dont get deactivated, just need a reset
    public List<GameObject> resetEnemy = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(nuts == true)
        {
            //This will remove the platanos and other items from the list to clear it up using the nuts boolean
            for (int i = 0; i < platanos.Count; i++)
            {
                platanos.RemoveAt(i);
            }

            for (int i = 0; i < barrels.Count; i++)
            {
                barrels.RemoveAt(i);
            }

            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList.RemoveAt(i);
            }

            //Will run the function clearList (which just sets nuts to false) so that it wont forever remove platanos and items from the list as you collect them, 1 second after they've been removed
            Invoke("clearList", .1f);

            //nuts = false;

            
        }
    }

    //The healthmanager will call this function every time the player dies
    public void respawnPlatanos()
    {
        //for every platano and item in the list, respawn it by setting the gameobject active
        for (int i = 0; i < platanos.Count; i++)
        {
            platanos[i].SetActive(true);
        }

        for (int i = 0; i < barrels.Count; i++)
        {
            barrels[i].SetActive(true);
        }

        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetActive(true);
        }

        //For objects that dont setactive false
        // for (int i = 0; i < resetEnemy.Count; i++)
        // {
        //     resetEnemy[i].SetActive(true);
        // }
        
        nuts = true;

        


        // //after reactivating the platanos, remove them from the list to prevent an infinite list
        // for (int i = 0; i < platanos.Count; i++)
        // {
        //     platanos.RemoveAt(i);
        // }
    }

    public void clearList()
    {
        nuts = false;
    }
}
