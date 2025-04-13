using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HealthSystem PlayerHealth;
    public AudioClip damageSound;

    // Update is called once per frame
    void Update()
    {
        // placeholder for testing
        if (Input.GetKeyDown(KeyCode.H))
        {
            takeDamage(1);
        }
    }

    public void takeDamage(int damage) {
        PlayerHealth.LoseHealth(damage);
        SoundFXManager.Instance.PlayAudioClip(damageSound, transform, 0.653f, 1f);
        if (PlayerHealth.GetHealth() == 0) {
            Die();
        }
    }

    public void Heal(int healAmount) {
        PlayerHealth.Heal(healAmount);
    }

    void Die() {
        // insert death code here
        Debug.Log("You are dead!");
    }
}
