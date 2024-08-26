using UnityEngine;

public class TongueActivators : MonoBehaviour
{
    public bool canSpit;
    public bool rest;

    //Using an array to store all the tongues. The editor will let us put them in one by one.
    public GameObject[] tongues;
    GameObject currentTongue;
    int index;

    GameObject allTongues;

    public float startSpitTime;
    float spitTime;
    public float startRestTime;
    float restTime;

    // Start is called before the first frame update
    void Start()
    {
        canSpit = false;
        rest = false;

        for (int i = 0; i < tongues.Length; i++)
        {
            GameObject allTongues = tongues[i];
            allTongues.SetActive(false);
        }

        spitTime = startSpitTime;
        restTime = startRestTime;
    }

    // Update is called once per frame
    void Update()
    {
        //To pick a random tongue and stick it out/set it active
        if (spitTime == startSpitTime) {
            if (canSpit == true){
                index = Random.Range (0, tongues.Length);
                currentTongue = tongues[index];
                currentTongue.SetActive(true);
                canSpit = false;
            }
        }


        if (canSpit == false && rest == false) {
            spitTime -= Time.deltaTime;
        }

        if (spitTime <= 0){
            rest = true;
            spitTime = startSpitTime;
        }

        // if (spitTime <= 0){
        //     rest = true;
        //     spitTime = startSpitTime;
        // } else {
        //     //spitTime -= Time.deltaTime;
        // }
        

        //if rest equals true, deactivate all tongues for a set time, then make canSpit true
        if (rest == true) {
            for (int i = 0; i < tongues.Length; i++)
            {
                GameObject allTongues = tongues[i];
                allTongues.SetActive(false);
            }
            restTime -= Time.deltaTime;
            if (restTime <= 0){
                canSpit = true;
                rest = false;
            }

        }
    }

    // void PickARandomTongue(){
    //     // if (atRest == true){
    //     //     // index = Random.Range (0, tongues.Length);
    //     //     // currentTongue = tongues[index];
    //     //     // currentTongue.SetActive(true);
    //     //     allTongues.SetActive(true);
    //     // } else {
    //     //     allTongues.SetActive(false);
    //     // }
    // }
}
