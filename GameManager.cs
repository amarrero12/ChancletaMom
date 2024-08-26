using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public so we can see how much gold we have collected in the editor
    public int currentGold;
    public Text goldText;

    // public GameObject Barrel1;
    // public GameObject Barrel2;
    // public GameObject Barrel3;
    // public GameObject Barrel4;

    // public GameObject platano;

    public Transform gameCam;
    public Transform player;
    public Transform targets;
    public bool startAtHub;
    public bool level2Start;
    public bool level3Start;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the saved platano count (from playerController)
        //goldText.text = PlayerPrefs.GetInt("Platanos", 0).ToString();
        currentGold = PlayerPrefs.GetInt("Platanos", 0);

        if(startAtHub == true)
        {
            gameCam.position = new Vector3(0f, 3.48f, -23.085f);
            player.position = new Vector3(-0.06f, 0.941f, -15.45f);
        }

        if (level2Start == true)
        {
            gameCam.position = new Vector3(-0.14f, 4.73f, 18.08f);
            player.position = new Vector3(-0.2f, 0.8f, 25.71501f);
        }

        if (level3Start == true)
        {
            gameCam.position = new Vector3(1.51f, 4.72f, 85.8f);
            player.position = new Vector3(1.45f, 1.86f, 96.88f);
            targets.position = new Vector3(1.45f, 4.3f, 99.53f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = "" + currentGold;
        //Reset the platano count
        if(Input.GetKey(KeyCode.V))
        {
            PlayerPrefs.DeleteKey("Lives");
            PlayerPrefs.DeleteKey("Platanos");
        }
    }

    //A function for adding gold to our total when we pick it up
    public void AddGold(int goldToAdd)
    {
        currentGold += goldToAdd;
        //Update our text score in the UI
        // goldText.text = "Gold: " + currentGold + "!";
        goldText.text = "" + currentGold;

        if(currentGold == 100)
        {
            GetComponent<LivesManager>().lives ++;
            currentGold = 0;
        }
    }
}
