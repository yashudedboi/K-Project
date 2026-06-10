using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Reference to your UI panels
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject pausePanel;
    public playerhealth playerhealth;

    void Start()
    {
        // Resume game time in case it was paused from a previous game over/win
        Time.timeScale = 1f;
        pausePanel.SetActive(false);

        // Ensure the game starts with correct screens visible
        gameplayPanel.SetActive(true);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShowPauseScreen();
            Time.timeScale = 0f;
        }
    }
    public void ShowPauseScreen()
    {
        gameplayPanel.SetActive(false);
        pausePanel.SetActive(true);
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
    public void ResumeButton()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameplayPanel.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}