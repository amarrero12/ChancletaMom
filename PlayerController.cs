using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    //public float dashForce;
    public CharacterController controller;
    
    public Vector3 moveDirection;
    public float gravityScale;

    public Animator anim;

    public Transform pivot;
    public float rotateSpeed;

    public GameObject playerModel;

    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

    public GameObject platano;

    //For Riding Level Purposes Only!
    public bool autoRun;
    public float newSpeed;
    
    //So that in level 2, we disable the attack until the player crashes in and out of the chancleta store
    public PlayerAttack atkScript;
    //In this level, we will leave the bool false in the engine, only in the engine so that nothing in the script
    //messes with it in other levels. This bool will be used to make the PlayerAttack script enabled after crashing into
    //the chancleta store, allowing the attacking code to work and be used to attack the henchmen. Also use this bool
    //enable the Donkey attack animation to run. Be sure to leave the bool true in other levels as well as the PlayerAttack script. 
    public bool canAttack;
    public DonkeyAttack donkeyScript;
    //do an "if player crashes into chancleta store, canAttack = true"

    // //For gamepad input
    // PlayerControls controls;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // void Awake()
    // {
    //     controls = new PlayerControls();

    //     controls.Gameplay.Jump.performed += ctx => Jump();
    // }
    
    
    // Update is called once per frame
    void Update()
    {
        Jump();
        //so that we can ONLY move when we're not knock backed, doesn't include gravity bc we want to be able to fall even if we are knock backed
        if (knockBackCounter <= 0)
        {
            //move using wasd keys times speed
            //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);

            //keep the old y so we can use it later to jump properly
            float yStore = moveDirection.y;

            //float zStore = moveDirection.z;

            //will move the gameobject "forward" in the direction that it/camera is facing, forward means the blue axis, vertical as in W and S
            //transform.right does the same for moving left and right, Horizontal Input as in A and S
            //use GetAxis Raw to stop player sliding

            if (autoRun == true)
            {
                moveDirection = (transform.forward * moveSpeed) + (transform.right * Input.GetAxisRaw("Horizontal") * moveSpeed);
            } else
            {
                moveDirection = (transform.forward * Input.GetAxisRaw("Vertical") * moveSpeed) + (transform.right * Input.GetAxisRaw("Horizontal") * moveSpeed);
            }
            

            //moveDirection = (transform.forward * Input.GetAxis("Vertical") * moveSpeed) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed);
            
            //so that when we move diagnally, we dont move at a faster rate than normal due to the x and y values combining, normalized will change the value to a lesser number
            //so that we go at the same speed as the other axes
            moveDirection = moveDirection.normalized * moveSpeed;

            //bring back old y so we can jump properly
            moveDirection.y = yStore;

            //moveDirection.z = zStore;

            //if player is on the floor, set moveDirection Y to zero so it's not constantly decreasing, forcing the player to slam down
            if (controller.isGrounded)
            {
                //Jump();
            }

            // //For the dash move
            // if (controller.isGrounded == false)
            // {
            //     moveDirection.z = 0f;
            //     if (Input.GetButtonDown("Jump"))
            //     {
            //         moveDirection.z = dashForce;
            //     }
            // }

        } else
        {
            //does a countdown after we're knock backed
            knockBackCounter -= Time.deltaTime;
        }
        

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        //Move the player in different directions based on camera look direction (pivot)
        //If we press a movement button on Horizontal OR Vertical, rotate the player the way the pivot is rotated
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            //So we can run in other directions instead of just forward
            //LookRotation gives us a specific place to look at
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            //gamesplusjames video #9 19:15, rotate player model
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        //set the animation bool to true to idle when grounded using the isGrounded function we have set already
        anim.SetBool("isGrounded", controller.isGrounded);
        //use mathf abs to get the absolute value of whether or not we're pressing the button no matter which way we're going to do the run animation
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
        //Debug.Log(moveDirection.y);

        //For Level 2 re-enabling the attack script and donkey script w/ animation
        if(canAttack == true)
        {
            atkScript.enabled = true;
            donkeyScript.enabled = true;
        }
    }

    public void Jump()
    {
        if(knockBackCounter <= 0)
        {
            if(controller.isGrounded)
            {
                //use next code only if you want player to keep going in midair after you let go of a directional button
                //moveDirection = (transform.forward * Input.GetAxisRaw("Vertical") * moveSpeed) + (transform.right * Input.GetAxisRaw("Horizontal") * moveSpeed);
                
                moveDirection.y = 0f;
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }
        }
    }



    public void Knockback(Vector3 direction)
    {
        //set the knockback counter equal to the starting knockback time we have set. Do it here instead of start so that we can move and do normal things
        //when the knockback counter is less than 0
        knockBackCounter = knockBackTime;

        //creates a vector 3 for us to go to when we're knocked back
        //we're getting it from the hurtplayer script which feeds it to the healthmanager script
        //direction = new Vector3 (1f, 1f, 1f);

        //knocks us back with our moveDirection by multiplying our direction vector 3 with the knockback force we have set
        moveDirection = direction * knockBackForce;
        //no matter what we always get hit into the air when were knocked back
        moveDirection.y = knockBackForce;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "bounceable")
        {
            //makes the player bounce after jumping on the enemy
            moveDirection.y = jumpForce;
            // Instantiate(platano, other.gameObject.transform.position, Quaternion.Euler(new Vector3(0, -90, 90)));
            // Destroy(other.gameObject);

            //run the break function in the barrels's script
            other.gameObject.GetComponent<BreakBarrel>().Break();
        }

        if(other.gameObject.tag == "bounceabledog")
        {
            //makes the player bounce after jumping on the enemy
            moveDirection.y = jumpForce;
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<Enemy>().Die();

        }

        if(other.gameObject.tag == "crashinto")
        {
            // Instantiate(platano, other.gameObject.transform.position, Quaternion.Euler(new Vector3(0, -90, 90)));
            // Destroy(other.gameObject);
            other.gameObject.GetComponent<BreakBarrel>().Break();
        }

        if(other.gameObject.tag == "bounceableLvl2")
        {
            //makes the player bounce after jumping on the cart
            moveDirection.y = jumpForce + 3;
        }

        if(other.gameObject.tag == "chancletastore")
        {
            canAttack = true;
        }

        //switch to next scene/level
        if(other.gameObject.tag == "levelEnd")
        {
            //Saves the platano count
            PlayerPrefs.SetInt("Platanos", GameObject.Find("GameManager").GetComponent<GameManager>().currentGold);
            //Saves the lives count
            PlayerPrefs.SetInt("Lives", GameObject.Find("GameManager").GetComponent<LivesManager>().lives);
            GameObject.Find("SceneManager").GetComponent<switchScene>().LoadNextScene();
        }


    }
}
