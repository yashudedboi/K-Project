using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private Vector3 startPosition;
	public float range;

	void Start()
	{
		startPosition = transform.position;
	}

	void Update()
	{
		float distance = Vector3.Distance(startPosition, transform.position);

		if (distance > range)
		{
			Destroy(gameObject);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Enemy")|| collision.gameObject.CompareTag("Enviorment"))
		{
			Destroy(gameObject);
		}
	}

}
