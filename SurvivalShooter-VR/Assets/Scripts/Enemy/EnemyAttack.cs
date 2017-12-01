using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 1.0f;
    public int attackDamage = 5;

    GameObject player;
    GameObject Wood;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;
    bool woodHit;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;         
        }
        else if(other.gameObject.tag == "Wood")
        {
          woodHit = true;
          enemyHealth.Death();       
        } 
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Wood")
            {
              woodHit = true;
              enemyHealth.Death();       
            } 
    }

    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
        else if(other.gameObject == Wood)
        {
          woodHit = false;        
        } 
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange)/* && enemyHealth.currentHealth > 0*/
        {
            Attack ();
        }

        //if(playerHealth.currentHealth <= 0)
        //{
        //    anim.SetTrigger ("PlayerDead");
        //}
    }


    void Attack ()
    {
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
