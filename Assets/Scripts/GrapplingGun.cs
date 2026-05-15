using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
	private LineRenderer lr;
	public GameObject gunEquipped;
	private Vector3 grapplepoint;
	public LayerMask whatIsGrappleable;
	public Transform gunTip, camera, player;
	private float maxDistance = 100f;
	private SpringJoint joint;
	public EquipScript equipScript;


	void Awake()
	{
		lr = GetComponent<LineRenderer>();
	}
	void Update()
	{
		if (Input.GetMouseButtonDown(0) && equipScript.IsObjectEquipped() == true && gunEquipped.transform.parent != null)
		{
			StartGrapple();

		}
		else if (Input.GetMouseButtonUp(0))
		{
			StopGrapple();
		}
	}
	private void LateUpdate()
	{
		DrawRope();
	}
	void StartGrapple()
	{
		RaycastHit hit;
		if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
		{
			grapplepoint = hit.point;
			joint = player.gameObject.AddComponent<SpringJoint>();
			joint.autoConfigureConnectedAnchor = false;
			joint.connectedAnchor = grapplepoint;

			float distanceFromPoint = Vector3.Distance(player.position, grapplepoint);

			joint.maxDistance = distanceFromPoint * 0.8f;
			joint.minDistance = distanceFromPoint * 0.25f;

			joint.spring = 4.5f;
			joint.damper = 7f;
			joint.massScale = 4.5f;

			lr.positionCount = 2;
		}
	}

	void DrawRope()
	{
		if (!joint) return;


		lr.SetPosition(0, gunTip.position);
		lr.SetPosition(1, grapplepoint);

	}
	void StopGrapple()
	{
		lr.positionCount = 0;
		Destroy(joint);
	}
	public bool IsGrappling()
	{
		return joint != null;
	}
	public Vector3 GetGrapplePoint()
	{
		return grapplepoint;
	}
}


