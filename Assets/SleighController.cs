using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SleighController : MonoBehaviour
{
	private Transform thisTransform;
	private Rigidbody thisRigidbody;


	public int controllerNumber = 1;
	public XBoxCtrlInputs inputs = null;

	// 0 = move; 1= front; 2=left; 3 = right; 4 = back
	public Transform[] anchors;
	public SleighComponent[] sleighParts;

	public float slideFriction = 8f;
	public float rotateSpeed = 5;
	public float forwardSpeed = 12;
	public float reverseSpeed = 5;
	public float drivePower = 10;
	public float actualSpeed;

	public Vector3 relativeVel;
	public Vector3 relativeAngularVel;

	// Use this for initialization
	void Start ()
	{
		thisRigidbody = GetComponent<Rigidbody> ();
		thisTransform = transform;
		inputs = ControllerInputManager.GetInputsForControllerNumber (controllerNumber);
		PlayerManager.singleton.AddNewSleigh (this);
		thisRigidbody.maxAngularVelocity = 2.5f;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnCollisionEnter (Collision col)
	{

		if (col.gameObject.CompareTag ("Elf")) {

			Collider collider = col.contacts [0].thisCollider;

			for (int i = 0; i < sleighParts.Length; i++) {
				if (sleighParts [i].coll == collider) {
					sleighParts [i].EnterCollision (col.gameObject.GetComponent<Elf> ());
				}
			}
		}
	}

	void OnCollisionStay (Collision col)
	{

		if (col.gameObject.CompareTag ("Elf")) {
			Collider collider = col.contacts [0].thisCollider;

			for (int i = 0; i < sleighParts.Length; i++) {
				if (sleighParts [i].coll == collider) {
					sleighParts [i].StayCollision (col.gameObject.GetComponent<Elf> ());
				}
			}
		}
	}

	void FixedUpdate ()
	{


		relativeVel = thisTransform.InverseTransformVector (thisRigidbody.velocity);


		float throttle = inputs.leftStickY * Time.deltaTime * drivePower;

		float currentMaxSpeed = 0;
		if (inputs.leftStickY > 0) {
			currentMaxSpeed = forwardSpeed;
		} else if (inputs.leftStickY < 0) {
			currentMaxSpeed = reverseSpeed;
		}
		// drive...
		Vector3 velToAdd = Vector3.forward * throttle;
		Vector3 finalVelocity = relativeVel + velToAdd;
		if (finalVelocity.magnitude > currentMaxSpeed) {
			velToAdd = Vector3.zero;
			thisRigidbody.AddRelativeForce (Vector3.forward * -relativeVel.z * 0.5f * Time.deltaTime, ForceMode.VelocityChange);
		}
		actualSpeed = relativeVel.magnitude;
		thisRigidbody.AddRelativeForce (velToAdd, ForceMode.VelocityChange);

		if (currentMaxSpeed == 0) {
			// sloow dooown...
			thisRigidbody.AddRelativeForce (Vector3.forward * -relativeVel.z * 0.5f * Time.deltaTime, ForceMode.VelocityChange);
		}

		relativeAngularVel = thisTransform.InverseTransformVector (thisRigidbody.angularVelocity);
		// slide...
		if (inputs.leftStickX != 0) {
			thisRigidbody.AddRelativeTorque (Vector3.up * inputs.leftStickX * Time.deltaTime * rotateSpeed, ForceMode.VelocityChange);
		} else {
			thisRigidbody.AddRelativeTorque (Vector3.up * -relativeAngularVel.y * Time.deltaTime * slideFriction, ForceMode.VelocityChange);
		}
		thisRigidbody.AddRelativeForce (Vector3.right * -relativeVel.x * slideFriction * Time.deltaTime, ForceMode.VelocityChange);

		// align to surface
		thisRigidbody.MoveRotation (Quaternion.Slerp (thisTransform.rotation, Quaternion.FromToRotation (thisTransform.up, Vector3.up) * thisTransform.rotation, 5 * Time.deltaTime));
	}
}
