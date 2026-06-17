using UnityEngine;
using UnityEngine.SceneManagement;

public class Key : MonoBehaviour
{
	public void OnCollisionEnter(Collision collision)
	{
		if ((collision.gameObject.CompareTag("Exit")))
		{
            Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = true;

            SceneManager.LoadScene("End");
		}
	}
}
