using TMPro;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    [SerializeField] GameObject referenceprojectile;
    [SerializeField] Transform GunTip;
    public GameObject isGunEquipped;

    [Header("Shotgun Settings")]
    [SerializeField] int pelletCount = 8;       // Number of projectiles per shot
    [SerializeField] float spreadAngle = 5.0f;  // Max angle deviation in degrees
    [SerializeField] float projectileSpeed = 50.0f;

    void Update()
    {
        // Added a null check for safety before checking the parent
        if (Input.GetMouseButtonDown(0) && isGunEquipped != null && isGunEquipped.transform.parent != null)
        {
            OnFire();
        }
    }

    void CreateProjectile(Vector3 targetDestination)
    {
        GameObject projectile = Instantiate(referenceprojectile, GunTip.position, Quaternion.identity);
        Destroy(projectile, 1f);

        // Calculate direction towards the destination
        Vector3 baseDirection = (targetDestination - GunTip.position).normalized;

        // Create a random rotation within a cone based on spreadAngle
        Quaternion randomSpread = Quaternion.Euler(
            Random.Range(-spreadAngle, spreadAngle),
            Random.Range(-spreadAngle, spreadAngle),
            0
        );

        // Apply the random spread to the base direction
        Vector3 finalDirection = randomSpread * baseDirection;

        // Apply force to the Rigidbody
        if (projectile.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.AddForce(finalDirection * projectileSpeed, ForceMode.Impulse);
        }
    }

    void OnFire()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 mainDestination;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            mainDestination = hit.point;
        }
        else
        {
            mainDestination = ray.GetPoint(1000);
        }

        // Loop to create multiple pellets in a single frame
        for (int i = 0; i < pelletCount; i++)
        {
            CreateProjectile(mainDestination);
        }
    }
}