using UnityEngine;
using System.Collections;

public class Elf : MonoBehaviour
{
	Rigidbody thisRigidbody;
	Transform thisTransform;
	public Transform animTransform;
	Vector3 santaDir = Vector3.zero;
	Vector3 moveDir = Vector3.forward;
	public float runSpeed = 2.75f;
	public float walkSpeed = 1;
	public float distToSanta;

	static float distanceToFlee = 10;
	Transform nearestSanta;

	public ElfType elfType = ElfType.bad;

	// Use this for initialization
	void Start ()
	{
		thisTransform = this.transform;
		thisRigidbody = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void FixedUpdate ()
	{
		// run randonly unless santa is near
		GetDistToNearestSanta ();
		if (distToSanta < distanceToFlee) {
			// work out direction to santa
			santaDir = (thisTransform.position - nearestSanta.position).normalized;
			// now just run in this direction
			moveDir *= runSpeed;
		} else {
			// just move forwards and turn randomly
			moveDir = thisTransform.TransformDirection (Vector3.forward) * walkSpeed;

		}
		moveDir.y = thisRigidbody.velocity.y;
		thisRigidbody.velocity = moveDir;
		// set the animation direction
		animTransform.rotation = Quaternion.LookRotation (thisRigidbody.velocity, Vector3.up);
	}

	void GetDistToNearestSanta ()
	{
		distToSanta = 10;
	}
}
