using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public PlayerController thePlayer;

    public float invincibilityLength;
    private float invincibilityCounter;

    public Renderer playerRenderer;
    private float flashCounter;
    public float flashLength = 0.1f;

    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnLength;

    public GameObject deathEffect;

    public Image blackScreen;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public float fadeSpeed;
    public float waitForFade;

    public bool rideLevel;

    //We need access to this script to reset the items when we die
    public EnemyManager itemManagerScript;
    

    // Start is called before the first frame update
    void Start()
    {
        //start the game at max health
        currentHealth = maxHealth;

        //search for the player controller at the start so we can always have access to it. We're calling for knockback here and not in hurtplayer script bc we don't
        //wanna have too many FindObjectOfType functions in one script bc it can slow down that way
        //commented out so we can just drag the player in through the editor, easier for future reference
        //thePlayer = FindObjectOfType<PlayerController>();

        //sets the respawn point as wherever the player is when the game starts
        respawnPoint = thePlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(respawnPoint);

        //once the counter is set to its length, begin the inv countdown aka invincibility frames and flash countdown
        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            
            //if the flash counter ends, set the renderer to the opposite of what it is and reset the counter. This is literally making the character flash. 
            //(its really switiching the renderer to the opposite of what it is every 0.1 seconds)
            if (flashCounter <= 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }

            //once the invisibility frames are over, make sure the renderer is on so that the player is visible in case if the frame counted ended with it off
            if (invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
        }

        if (isFadeToBlack)
        {
            //takes the color of the blackScreen and fades it from being transparent to being black (0 to 1) in a certain amount of time (fadespeed)
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            //if the color has reached black, stop fading and make the bool false
            if (blackScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }

        if (isFadeFromBlack)
        {
            //takes the color of the blackScreen and fades it from being black to being transparent (1 to 0) in a certain amount of time (fadespeed)
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            //if the color has reached transparent, stop fading and make the bool false
            if (blackScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
            }
        }
    }

    //when the player gets hurt, take damage and knockback
    public void HurtPlayer(int damage, Vector3 direction)
    {
        //allows us to only take damage when the invicibility counter is at 0/off
        if (invincibilityCounter <= 0)
        {
            currentHealth -= damage;

            //if player's health falls to zero, respawn, otherwise do knockback and all that other stuff
            if (currentHealth <= 0)
            {
                Respawn();
                GetComponent<LivesManager>().lives--;
                

            }
            else
            {
                if(rideLevel == false)
                {
                    //the vector 3 is given from the hurtplayer script, its coming from there and we will knockback the player into that direction
                    thePlayer.Knockback(direction);
                }

                //set the inv counter equal to the length we set when we get hit (to start countdown in update)
                invincibilityCounter = invincibilityLength;

                //turns off the player renderer when we get hit
                playerRenderer.enabled = false;

                //sets the flash counter equal to the length we set for the countdown
                flashCounter = flashLength;
            }
        }
    }

    public void Respawn()
    {
        // //sends the player straight to the respawn point and resets the health

        // //specific code needed to get the respawn to work in newer versions of Unity
        // //we have to disable the CharacterController before we respawn, and then enable it again right after
        // GameObject player = GameObject.Find("Player");
        // CharacterController charController  = player.GetComponent<CharacterController>();
                    
        // charController.enabled = false;
        // thePlayer.transform.position = respawnPoint; 
        // charController.enabled = true;

        // //thePlayer.transform.position = respawnPoint;
        // currentHealth = maxHealth;

        //if we're not already respawning, do the coroutine, this way we don't glitch the game and repeat ourselves since ienumerator operates on its own time kinda
        if (!isRespawning)
        {
            //Call upon the coroutine, all the previous code will be in there
            StartCoroutine("RespawnCo");
        }
    }

    //we're using a coroutine to count a few seconds after we die, before we respawn so its not so right away
    public IEnumerator RespawnCo()
    {
        //the script now knows we are currently respawning and will not repeat the coroutine from the void Respawn function, in case something else kills us too while we're respawning
        isRespawning = true;
        //completely disables the player object now that we are dead and respawning
        thePlayer.gameObject.SetActive(false);

        //do death particles when the player dies at his location
        Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);

        //count for a few seconds before we officially respawn
        yield return new WaitForSeconds(respawnLength);

        //start our fade out
        isFadeToBlack = true;

        //waiting for the fade to finish
        yield return new WaitForSeconds(waitForFade);

        //allows us to always finish fading? in case our fadespeed and wait for fade are really low, gamesplusjames 16 19:00
        isFadeToBlack = false;

        //start fading back to transparent (remove the black screen)
        isFadeFromBlack = true;

        //we have finished respawning (counting) and can now set it to false so that we can respawn (count) again if we die
        isRespawning = false;

        //now that we're done respawning (counting) re-enable the player
        thePlayer.gameObject.SetActive(true);

        //Let's actually respawn now
        //specific code needed to get the respawn to work in newer versions of Unity
        //we have to disable the CharacterController before we respawn, and then enable it again right after
        GameObject player = GameObject.Find("Player");
        CharacterController charController  = player.GetComponent<CharacterController>();
        charController.enabled = false;
        thePlayer.transform.position = respawnPoint; 
        charController.enabled = true;

        //reset the health
        currentHealth = maxHealth;

        //reset the platanos
        itemManagerScript.respawnPlatanos();


        //give invincibility frames upon respawning
        invincibilityCounter = invincibilityLength;
        playerRenderer.enabled = false;
        flashCounter = flashLength;

    }

    //when the player gets healed, heal their health
    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    //for changing where our respawnpoint is
    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }
}
