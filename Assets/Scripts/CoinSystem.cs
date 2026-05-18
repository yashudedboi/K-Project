using UnityEngine;
using TMPro;

public class CoinSystem : MonoBehaviour
{
    // Changed to 'public' so other scripts can find it easily, 
    // or you can still assign it in the Inspector.
    public TMP_Text coinText;

    // We removed the ShopMenu reference entirely from here!

    void Start()
    {
        // Optional safety check: Warns you in the editor if you forgot to assign the text box
        if (coinText == null)
        {
            Debug.LogError("CoinSystem: Missing TMP_Text component assignment!");
        }
    }

    /// <summary>
    /// Call this method whenever the player's coin count changes.
    /// </summary>
    public void UpdateCoinDisplay(int newCoinAmount)
    {
        if (coinText != null)
        {
            coinText.text = newCoinAmount.ToString();
        }
    }
}