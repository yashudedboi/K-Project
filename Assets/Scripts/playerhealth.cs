using UnityEngine;
using UnityEngine.SceneManagement;

public class playerhealth : MonoBehaviour
{
	public int health;
	public bool isPlayerDead;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	public void Start()
	{
		isPlayerDead = false;
	}
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
		isPlayerDead =true;
		Debug.Log("showinglosescreen");
		Destroy(gameObject);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
