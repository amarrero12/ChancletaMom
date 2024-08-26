using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandNThrowCat : MonoBehaviour
{
    public HurtPlayer2R hurtScript;

    void OnTriggerEnter (Collider other)
    {
        //Attack player with cat using bool connecting other script
        if(other.gameObject.tag == "Player")
        {
            hurtScript.standNThrow = true;
        }
    }
}
