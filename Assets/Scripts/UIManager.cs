using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Reference to your UI panels
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel; // 1. Added lose panel reference
    public playerhealth playerhealth;

    void Start()
    {
        // Resume game time in case it was paused from a previous game over/win
        Time.timeScale = 1f;

        // Ensure the game starts with correct screens visible
        gameplayPanel.SetActive(true);
        winPanel.SetActive(false);
        losePanel.SetActive(false); // 2. Hide lose panel at start
    }
    public void Update()
    {
        if (playerhealth.isPlayerDead == true)   
        {
            ShowLoseScreen();
        }
    }

    // Call this method when the player wins
    public void ShowWinScreen()
    {
        gameplayPanel.SetActive(false);
        winPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // 3. New method: Call this when the player loses (e.g., hits an enemy, falls in a pit)
    public void ShowLoseScreen()
    {
        gameplayPanel.SetActive(false);
        losePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    // 4. Handy shortcut for a "Retry" button on your lose screen
    public void RestartCurrentLevel()
    {
        // Reloads whatever level is currently active
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void NextLevel2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void NextLevel3()
    {
        SceneManager.LoadScene("Level 3");
    }
}