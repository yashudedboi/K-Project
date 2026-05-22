using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Reference to your UI panels
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject winPanel;

    void Start()
    {
        // Ensure the game starts with correct screens visible
        gameplayPanel.SetActive(true);
        winPanel.SetActive(false);
    }

    // Call this method when the player wins
    public void ShowWinScreen()
    {
        // Hide the gameplay UI
        gameplayPanel.SetActive(false);

        // Show the winning UI
        winPanel.SetActive(true);

        // Optional: Pause the game physics/time if needed
        Time.timeScale = 0f;
    }
    // Example trigger inside your player or game manager script
    void LevelComplete()
    {
        FindObjectOfType<UIManager>().ShowWinScreen();
    }
}