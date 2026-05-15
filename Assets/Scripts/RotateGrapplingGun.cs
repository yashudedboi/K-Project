using UnityEngine;

public class RotateGrapplingGun : MonoBehaviour
{
	public GrapplingGun grappling;
	private Quaternion desiredRotation;
	private float rotationSpeed = 5f;

	void Start()
	{
		desiredRotation = transform.rotation;
	}
	void Update()
	{
		if (grappling == null)
			return;

		if (!grappling.IsGrappling())
		{
			if (transform.parent != null)
			{
				desiredRotation = transform.parent.rotation;
			}
		}
		else
		{
			Vector3 dir = grappling.GetGrapplePoint() - transform.position;
			if (dir.sqrMagnitude > 0.001f)
			{
				desiredRotation = Quaternion.LookRotation(dir);
			}
		}

		transform.rotation = Quaternion.Lerp(transform.rotation,desiredRotation,Time.deltaTime * rotationSpeed);
	}
}