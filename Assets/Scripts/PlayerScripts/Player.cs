using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HealthSystem PlayerHealth;

    // Update is called once per frame
    void Update()
    {
        // placeholder for testing
        if (Input.GetKeyDown(KeyCode.H))
        {
            takeDamage(1);
        }
    }

    void takeDamage(int damage) {
        PlayerHealth.LoseHealth(damage);
        if (PlayerHealth.GetHealth() == 0) {
            Die();
        }
    }

    void Heal(int healAmount) {
        PlayerHealth.Heal(healAmount);
    }

    void Die() {
        // insert death code here
        Debug.Log("You are dead!");
    }
}
