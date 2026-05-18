using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.UI;
using UnityEngine.ProBuilder.MeshOperations;

public class playerKey : MonoBehaviour
{
    public bool IsKeyEquipped;
    private void Start()
    {
        IsKeyEquipped = false;
    }
    public void OnTriggerEnter(Collider collision)
    {
        if ((collision.gameObject.CompareTag("Key")))
        {
            IsKeyEquipped = true;
            Debug.Log("KEY EQUIPPED");
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Exit")))
        {
            if (IsKeyEquipped == true)
            {
                SceneManager.LoadScene("WinScreen");
            }
            else
            {
                Debug.Log("Get Key first");
            }
        }
    }
}
