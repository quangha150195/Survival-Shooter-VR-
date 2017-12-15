using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Animator blood;
    public bool isDead;
    bool damaged;

    public static PlayerHealth instance;

    void Awake ()
    {
        currentHealth = startingHealth;
        instance = this;
    }


    void Update ()
    {
        
    }


    public void TakeDamage (int amount)
    {
        blood.Play("Blood", -1, 0);
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        SoundController.Sound.PlayerHurt();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;
        PlayerController.instance.m_move = false;

        SoundController.Sound.PlayerDeath();
    }
}
