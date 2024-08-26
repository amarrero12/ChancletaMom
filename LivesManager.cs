using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    public int lives = 5;
    public Text livesText;

    // Start is called before the first frame update
    void Start()
    {
        lives = PlayerPrefs.GetInt("Lives", 5);
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "" + lives;
        if(lives < 0)
        {
            GameObject.Find("SceneManager").GetComponent<switchScene>().TryAgain();
            lives = 5;
        }
    }
}
