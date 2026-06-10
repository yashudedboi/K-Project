using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> checkPoints;
    [SerializeField] Vector3 vectorPoint;
    public int health = 100;
    public bool isPlayerDead;
    public Slider healthSlider;

    public void Start()
    {
        isPlayerDead = false;
        if (healthSlider != null)
        {
            healthSlider.maxValue = health;
            healthSlider.value = health;
        }
        // Set the initial spawn point so vectorPoint isn't (0,0,0) at the start
        vectorPoint = player.transform.position;
    }

    void Update()
    {
        if (isPlayerDead)
        {
            player.transform.position = vectorPoint;
            health = 100;
            if (healthSlider != null) healthSlider.value = health;

            isPlayerDead = false; // CRITICAL: Reset this so you can move again!
        }
    }

    // Use FixedUpdate or OnTriggerEnter for Trigger objects (like Checkpoints or phantom Lava)
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            vectorPoint = other.transform.position; // Save the checkpoint's position, not the player's current position
        }
        else if (other.gameObject.CompareTag("Lava"))
        {
            PlayerDied();
            Debug.Log("You Died in Lava!");
        }
    }

    // Use OnCollisionEnter for solid objects (like physical Projectiles)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject); // Destroy the projectile that hit you

            if (health > 10)
            {
                health -= 10;
                if (healthSlider != null) healthSlider.value = health;
            }
            else
            {
                PlayerDied();
            }
        }
    }

    void PlayerDied()
    {
        isPlayerDead = true;
    }
}