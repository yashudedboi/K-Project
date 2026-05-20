using UnityEngine;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject shopPanel;
    public Button[] buttons;
    public CoinSystem coinSystem;

    [Header("Shop Settings")]
    public int coins;
    public GameObject[] gunPrefabs;
    public Transform itemSpawnPoint;

    [Header("References")]
    public Transform playerReference;
    public Transform cameraReference; // Fixed typo from 'cameraRefernce'

    void Start()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (shopPanel != null)
            {
                if (shopPanel.activeSelf) // shop was active
                {
                    // close shop
                    shopPanel.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else // shop was not open
                {
                    // open shop
                    shopPanel.SetActive(true);
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                }
            }
        }
    }

    public void BuyShotGun()
    {
        if (coins >= 100)
        {
            coins -= 100;
            coinSystem.UpdateCoinDisplay(coins);
            SpawnGun(0);
        }
    }

    public void BuyMiniGun()
    {
        if (coins >= 120)
        {
            coins -= 120;
            coinSystem.UpdateCoinDisplay(coins);
            SpawnGun(1);
        }
    }

    public void BuyGrappleGun()
    {
        if (coins >= 200)
        {
            coins -= 200;
            coinSystem.UpdateCoinDisplay(coins);
            SpawnGun(2);
        }
    }

    public void CloseShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void OpenShop()
    {
        if (shopPanel != null)
        {
            shopPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    private void SpawnGun(int prefabIndex)
    {
        if (gunPrefabs != null && prefabIndex < gunPrefabs.Length && gunPrefabs[prefabIndex] != null && itemSpawnPoint != null)
        {
            // 1. Spawn the gun prefab
            GameObject newGun = Instantiate(gunPrefabs[prefabIndex], itemSpawnPoint.position, itemSpawnPoint.rotation);

            // 2. Connect Grappling Gun references safely (looking in children just in case)
            GrapplingGun grapplingGun = newGun.GetComponentInChildren<GrapplingGun>();
            if (grapplingGun != null)
            {
                grapplingGun.camera = cameraReference;

                // --- NEW CODE HERE ---
                // If your GrapplingGun script is the one that needs the EquipScript, 
                // we look for 'EquipScript' on your player reference and hand it over:
                if (playerReference != null)
                {
                    grapplingGun.equipScript = playerReference.GetComponent<EquipScript>();
                }
            }

            RotateGrapplingGun rgg = newGun.GetComponentInChildren<RotateGrapplingGun>();
            if (rgg != null && rgg.grappling != null)
            {
                rgg.grappling.player = playerReference;
            }
        }
    }
}