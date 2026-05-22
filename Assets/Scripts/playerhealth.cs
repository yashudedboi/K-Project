using UnityEngine;
using UnityEngine.SceneManagement;

public class playerhealth : MonoBehaviour
{
	public int health;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.CompareTag("Lava"))
		{
			playerDied();
			Debug.Log("YouDIed");
		}
		if (collision.gameObject.CompareTag("Projectile"))
		{
			if (health > 0 && health !=0)
			{
				health -= 10;
			}
			else
			{
				playerDied();
			}
			
		}
	}
	void playerDied()
	{
		Destroy(gameObject);
		SceneManager.LoadScene("LoseScreen");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
