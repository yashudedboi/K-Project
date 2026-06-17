using UnityEngine;
using UnityEngine.UI;

public class playerhealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool isPlayerDead;
    public Slider healthSlider;

    public void Start()
    {
        ResetHealth();
    }

    private void OnTriggerEnter(Collider collision)
    {
        // CheckPoint script handles Lava teleportation, 
        // but we still log it here if you want.
        if (collision.gameObject.CompareTag("Lava"))
        {
            Debug.Log("You Died in Lava!");
            isPlayerDead = true;
        }

        if (collision.gameObject.CompareTag("Projectile") && !isPlayerDead)
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthSlider != null) healthSlider.value = currentHealth;

        if (currentHealth <= 0)
        {
            isPlayerDead = true;
            Debug.Log("You Died from Damage!");
        }
    }

    public void ResetHealth()
    {
        isPlayerDead = false;
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth;
        }
    }
}