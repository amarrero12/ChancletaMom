using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    //So that we know if we want this attack animation in Level 2 (We don't)
    public bool atkAnimOk;

    public Animator animator;

    //To reference our AttackPoint
    public Transform attackPoint;

    //Create a range for our attack
    public float attackRange = 0.5f;

    //Create a layer mask so we can detect what enemies we are hitting
    public LayerMask enemyLayers;

    //for giving damage to enemies, public in case we wanna change it in the editor
    public int attackDamage = 1;

    //how many times we can attack per second
    public float attackRate = 2f;
    //stores the time where we can attack next
    float nextAttackTime = 0f;

    // //access player controller script so we can aceess the player's speed
    // public PlayerController playerController;

    public float startChanclaTime;
    public float chanclaTime;

    public bool isAttacking;

    void Start() 
    {
        chanclaTime = startChanclaTime;
    }

    // Update is called once per frame
    void Update()
    {

        //Limits us from spamming the attack
        //Time.time keeps track of our current time
        //if our time.time has passed our nextAttackTime(our time limit until we can attack) then we can attack again
        if (Time.time >= nextAttackTime)
        {
            //Push left mouse to attack
            if(Input.GetButtonDown("Fire1"))
            {
                Attack();

                if(chanclaTime == startChanclaTime)
                {
                    isAttacking = true;

                    //if we choose to attack then our nextAttackTime will be reset, aka we will have to wait. our time.time will add whatever 1 divided by our attackRate is (2 times per second is 1/2)
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
            
        }

        //This code allows the player's attack to last more than just 1 frame per attack
        if(isAttacking == true)
        {
            chanclaTime -= Time.deltaTime;
            Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);                
            foreach (Collider enemy in hitEnemies)
            {
                //access the enemy we hit's script and run the TakeDamage function with the 
                //set amount of damage to give (we're using our attackDamage integer)
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);

            }
                
            if(chanclaTime <= 0)
            {
                isAttacking = false;
                chanclaTime = startChanclaTime;
            }
        }
    }

    void Attack()
    {
        if (atkAnimOk == true)
        {
            //Play an attack animation
            animator.SetTrigger("Attack");
        }
        // //Play an attack animation
        // animator.SetTrigger("Attack");

        //Detect enemies in range of attack
        //Physics.OverlapSphere will create a sphere using our attack range to detect colliders/what we are hitting (our center point, our radius, filter out certain layers)
        //Collider[] hitEnemies array will store those colliders so we can go through them (in a list?)
        
        // Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        //Damage Them
        //create a loop where for each enemy we hit, they take damage
        // foreach (Collider enemy in hitEnemies)
        // {
        //     //access the enemy we hit's script and run the TakeDamage function with the set amount of damage to give (we're using our attackDamage integer)
        //     enemy.GetComponent<Enemy>().TakeDamage(attackDamage);

        // }
    }

    //this will draw a sphere in the editor so we can see our actual attack range (the center, the radius)
    void OnDrawGizmosSelected() 
    {
        //if attack point isn't assigned this next line will just return so we don't get any errors
        if (attackPoint == null)
            return;
            
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
