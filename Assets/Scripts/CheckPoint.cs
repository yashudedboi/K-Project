using UnityEngine;
using UnityEngine.UI;

// This guarantees an AudioSource will always exist on this GameObject
[RequireComponent(typeof(AudioSource))]
public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 vectorPoint;

    [Header("Spawn Settings")]
    [SerializeField] Vector3 spawnOffset = new Vector3(0, 1.5f, 0);

    public int health = 100;
    public bool isPlayerDead;
    public Slider healthSlider;

    private Rigidbody playerRb;

    private AudioSource soundPlayer; // Made private since we get it automatically
    public AudioClip Level1;
    public AudioClip Level2;
    public AudioClip Level3;

    public void Start()
    {
        // FIX: Correctly assign the AudioSource component to the variable
        soundPlayer = GetComponent<AudioSource>();

        isPlayerDead = false;
        if (healthSlider != null)
        {
            healthSlider.maxValue = health;
            healthSlider.value = health;
        }

        if (player != null)
        {
            vectorPoint = player.transform.position;
            playerRb = player.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (isPlayerDead)
        {
            player.transform.position = vectorPoint + spawnOffset;

            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector3.zero;
                playerRb.angularVelocity = Vector3.zero;
            }

            health = 100;
            if (healthSlider != null) healthSlider.value = health;

            isPlayerDead = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint1"))
        {
            vectorPoint = other.transform.position;
            soundPlayer.clip = Level1;
            soundPlayer.Play();
            Debug.Log("Checkpoint Saved!");
        }

        if (other.gameObject.CompareTag("CheckPoint2"))
        {
            vectorPoint = other.transform.position;
            soundPlayer.clip = Level2;
            soundPlayer.Play();
            Debug.Log("Checkpoint Saved!");
        }

        if (other.gameObject.CompareTag("CheckPoint3"))
        {
            vectorPoint = other.transform.position;
            soundPlayer.clip = Level3;
            soundPlayer.Play();
            Debug.Log("Checkpoint Saved!");
        }

        if (other.gameObject.CompareTag("Lava"))
        {
            PlayerDied();
        }
    }

    void PlayerDied()
    {
        isPlayerDead = true;
    }
}