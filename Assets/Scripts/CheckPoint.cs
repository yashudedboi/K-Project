using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 vectorPoint;

    [Header("Spawn Settings")]
    [SerializeField] Vector3 spawnOffset = new Vector3(0, 1.5f, 0);

    private Rigidbody playerRb;
    private playerhealth playerHealthScript; // Reference to the health script

    private AudioSource soundPlayer;
    public AudioClip Level1;
    public AudioClip Level2;
    public AudioClip Level3;

    public void Start()
    {
        soundPlayer = GetComponent<AudioSource>();

        if (player != null)
        {
            vectorPoint = player.transform.position;
            playerRb = player.GetComponent<Rigidbody>();
            playerHealthScript = player.GetComponent<playerhealth>();
        }
    }

    void Update()
    {
        if (playerHealthScript != null && playerHealthScript.isPlayerDead)
        {
            // 1. Teleport
            player.transform.position = vectorPoint + spawnOffset;

            // 2. FORCE physics to acknowledge the new position immediately
            Physics.SyncTransforms();

            // 3. Reset Physics
            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector3.zero;
                playerRb.angularVelocity = Vector3.zero;
                playerRb.WakeUp();
            }

            // 4. Reset health
            playerHealthScript.ResetHealth();
            Debug.Log("Teleport and Reset complete!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint1"))
        {
            vectorPoint = other.transform.position;
            PlaySound(Level1);
        }
        else if (other.gameObject.CompareTag("CheckPoint2"))
        {
            vectorPoint = other.transform.position;
            PlaySound(Level2);
        }
        else if (other.gameObject.CompareTag("CheckPoint3"))
        {
            vectorPoint = other.transform.position;
            PlaySound(Level3);
        }
        else if (other.gameObject.CompareTag("Lava"))
        {
            if (playerHealthScript != null)
            {
                playerHealthScript.isPlayerDead = true;
            }
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (soundPlayer != null && clip != null)
        {
            soundPlayer.clip = clip;
            soundPlayer.Play();
            Debug.Log("Checkpoint Saved!");
        }
    }
}