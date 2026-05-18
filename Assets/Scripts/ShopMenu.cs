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
                shopPanel.SetActive(!shopPanel.activeSelf);
            }
        }

        if (shopPanel != null && shopPanel.activeSelf && buttons != null && buttons.Length >= 3)
        {
            buttons[0].interactable = coins >= 100;
            buttons[1].interactable = coins >= 120;
            buttons[2].interactable = coins >= 200;
        }
    }

    public void BuyShotGun() { if (coins >= 100) { coins -= 100; coinSystem.UpdateCoinDisplay(coins); SpawnGun(0); } }
    public void BuyMiniGun() { if (coins >= 120) { coins -= 120; coinSystem.UpdateCoinDisplay(coins); SpawnGun(1); } }
    public void BuyGrappleGun() { if (coins >= 200) { coins -= 200; coinSystem.UpdateCoinDisplay(coins); SpawnGun(2); } }

    private void SpawnGun(int prefabIndex)
    {
        if (gunPrefabs != null && prefabIndex < gunPrefabs.Length && gunPrefabs[prefabIndex] != null && itemSpawnPoint != null)
        {
            Instantiate(gunPrefabs[prefabIndex], itemSpawnPoint.position, itemSpawnPoint.rotation);
        }
    }
}