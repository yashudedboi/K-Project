using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 vectorPoint;

    [Header("Spawn Settings")]
    // This will lift the player up on the Y axis so they don't spawn inside the box
    [SerializeField] Vector3 spawnOffset = new Vector3(0, 1.5f, 0);

    public int health = 100;
    public bool isPlayerDead;
    public Slider healthSlider;

    private Rigidbody playerRb;

    public void Start()
    {
        isPlayerDead = false;
        if (healthSlider != null)
        {
            healthSlider.maxValue = health;
            healthSlider.value = health;
        }
        vectorPoint = player.transform.position;
        playerRb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isPlayerDead)
        {
            // Teleport to the checkpoint PLUS the safety offset
            player.transform.position = vectorPoint + spawnOffset;

            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector3.zero; // Use playerRb.velocity on older Unity versions
                playerRb.angularVelocity = Vector3.zero;
            }

            health = 100;
            if (healthSlider != null) healthSlider.value = health;

            isPlayerDead = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            // Save the base position of the checkpoint box
            vectorPoint = other.transform.position;
            Debug.Log("Checkpoint Saved!");
        }

        if (other.gameObject.CompareTag("Lava"))
        {
            PlayerDied();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            // Save the base position of the checkpoint box
            vectorPoint = collision.transform.position;
            Debug.Log("Checkpoint Saved!");
        }

        if (collision.gameObject.CompareTag("Lava"))
        {
            PlayerDied();
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
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