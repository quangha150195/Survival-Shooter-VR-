using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth ;
    public int currentHealth;
    public Slider healthSlider;
    public Animator blood;
    public bool isDead;

    public static PlayerHealth instance;

    void Awake ()
    {
        currentHealth = startingHealth;
        instance = this;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage (int amount)
    {
        blood.Play("Blood", -1, 0);

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

    void OnCollisionEnter (Collision other)
    {
        if(other.gameObject.tag == "ItemHealth")
        {
          currentHealth += 15;
          healthSlider.value = currentHealth;
          Destroy(other.gameObject);
        }
    }
}