using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class MiniGun : MonoBehaviour
{
    [SerializeField] GameObject referenceprojectile;
    [SerializeField] Transform GunTip;
    public GameObject isGunEquipped;
    public float range;

    Vector3 destination;
    void Update()
    {
        if (Input.GetMouseButton(0) && isGunEquipped.transform.parent != null)
        {
            OnFire();
        }
    }
    void CreateProjectile()
    {
        GameObject projectile = Instantiate(referenceprojectile, GunTip.position, Quaternion.identity);
        Destroy(projectile, 1);
        projectile.GetComponent<Rigidbody>().AddForce((destination - projectile.transform.position).normalized * 50.0f, ForceMode.Impulse);
    }

    void OnFire()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }
        CreateProjectile();
    }

}
