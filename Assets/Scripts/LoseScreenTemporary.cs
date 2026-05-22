using UnityEngine;
using UnityEngine.SceneManagement;
public class LoseScreenTemporary : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }

    // Update is called once per frame
    public void LoseScreen()
    {
        SceneManager.LoadScene("LoseScreen");
    }
}
