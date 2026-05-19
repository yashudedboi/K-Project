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

    public void BuyShotGun() { if (coins >= 100) { coins -= 100; coinSystem.UpdateCoinDisplay(coins); SpawnGun(0); } }
    public void BuyMiniGun() { if (coins >= 120) { coins -= 120; coinSystem.UpdateCoinDisplay(coins); SpawnGun(1); } }
    public void BuyGrappleGun() { if (coins >= 200) { coins -= 200; coinSystem.UpdateCoinDisplay(coins); SpawnGun(2); } }

    private void SpawnGun(int prefabIndex)
    {
        if (gunPrefabs != null && prefabIndex < gunPrefabs.Length && gunPrefabs[prefabIndex] != null && itemSpawnPoint != null)
        {
            // 1. spawn a gun, and get a reference to the newly instantiated object (newGun)
            GameObject newGun = Instantiate(gunPrefabs[prefabIndex], itemSpawnPoint.position, itemSpawnPoint.rotation);

            // 2. connect references
            // get the parent's RotateGrapplingGun component, because it has    a reference to the GrapplingGun component/script on the child, which is missing references (player, equipscript, ...)
            RotateGrapplingGun rgg = newGun.GetComponent<RotateGrapplingGun>();


            rgg.grappling.player = playerReference;
        }
    }
}