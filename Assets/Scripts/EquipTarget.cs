using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipScript : MonoBehaviour
{
	//add stuff that should me called again and again under this
	public Transform PlayerTransform;
	public GameObject currentlyEquippedGun;
	public bool isObjectEquipped;
	public Camera Camera;
	public float range;
	public float open = 100f;
	float radius = 0.5f;

	// Start is called before the first frame update
	void Start()
	{
		//Gun.GetComponent<Rigidbody>().isKinematic = true;
		isObjectEquipped = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown("f"))
		{
			UnequipObject();
			Shoot();
		}
	}

	public void Shoot()
	{
		RaycastHit[] hits = Physics.SphereCastAll(Camera.transform.position,radius,Camera.transform.forward,range);

		if (hits.Length > 0)
		{
			foreach (RaycastHit hit in hits)
			{
				Target target = hit.transform.GetComponent<Target>();
				if (target != null)
				{
					EquipObject(target);
					break;
				}
			}
		}
	}

	void UnequipObject()
	{
		PlayerTransform.DetachChildren();
		if (currentlyEquippedGun != null)
		{
			currentlyEquippedGun.GetComponent<Rigidbody>().isKinematic = false;
		}
		currentlyEquippedGun = null;
		isObjectEquipped = false;
		Debug.Log("UnequipObject");
	}

	public void EquipObject(Target gun)
	{
		currentlyEquippedGun = gun.gameObject;
		gun.GetComponent<Rigidbody>().isKinematic = true;
		gun.transform.position = PlayerTransform.transform.position;
		gun.transform.rotation = PlayerTransform.transform.rotation;
		gun.transform.SetParent(PlayerTransform);
		isObjectEquipped = true;
		Debug.Log("EquipObject");
	}

	public bool IsObjectEquipped()
	{
		/*bool isObjectEquipped = gun.transform.parent!= null;
		return isObjectEquipped;*/

		return isObjectEquipped;
	}
}