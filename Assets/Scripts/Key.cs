using UnityEngine;

public class Key : MonoBehaviour
{
	public GameObject key;
	public void OnTriggerEnter(Collider collision)
	{
		if ((collision.gameObject.CompareTag("Player")))
		{
			Destroy(key);
		}
	}
}
