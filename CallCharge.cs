using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallCharge : MonoBehaviour
{
    void Start()
    {

    }
    //This script allows the charging burglar script to make them know that the player has touched the platform
    //and they can begin charging

    public ChargeBurglar chargeScript;
    public ChargeBurglar chargeScript2;
    public ChargeBurglar chargeScript3;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            chargeScript.playerClimbed = true;
            chargeScript2.playerClimbed = true;
            chargeScript3.playerClimbed = true;
        }
    }
}
