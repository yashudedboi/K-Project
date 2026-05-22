using UnityEngine;
// Note: Removed unnecessary/broken using statements to keep it clean

public class playerKey : MonoBehaviour
{
    public bool IsKeyEquipped;

    // 1. Add a reference to your UIManager script
    public UIManager uiManager;

    private void Start()
    {
        IsKeyEquipped = false;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            IsKeyEquipped = true;
            Debug.Log("KEY EQUIPPED");
            Destroy(collision.gameObject); // Optional: destroys the key object so you "pick it up"
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Exit"))
        {
            if (IsKeyEquipped == true)
            {
                // 2. Replace the SceneManager line with this:
                if (uiManager != null)
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    uiManager.ShowWinScreen();
                }
                else
                {
                    Debug.LogError("UIManager is missing from the PlayerKey script in the Inspector!");
                }
            }
            else
            {
                Debug.Log("Get Key first");
            }
        }
    }
}