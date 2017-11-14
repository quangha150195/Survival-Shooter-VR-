using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public AudioClip deathClip;
    public Animator blood;
    AudioSource playerAudio;
    public bool isDead;
    bool damaged;

    public static PlayerHealth instance;

    void Awake ()
    {
        playerAudio = GetComponent <AudioSource> ();
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

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;
        PlayerController.instance.m_move = false;

        playerAudio.clip = deathClip;
        playerAudio.Play ();
    }
}
