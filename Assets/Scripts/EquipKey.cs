using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipKey : MonoBehaviour
{
	public bool isKeyEquipped;
	public float range;
	public float open = 100f;
	float radius = 0.5f;

	// Start is called before the first frame update
	void Start()
	{
		isKeyEquipped = false;
	}

	// Update is called once per frame
	public void OnCollisionEnter(Collision collision)
	{
		if ((collision.gameObject.CompareTag("Key")))
		{
			EquipKeyObject();
		}
	}


	public void EquipKeyObject()
	{ 
		isKeyEquipped = true;
		Debug.Log("EquipKey");
	}

	public bool IsKeyEquipped()
	{

		return isKeyEquipped;
	}
}