using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

	public Transform target;

	public float distance = 6;
	public float height = 20;
	public float smoothSpeed = 5;
	private Vector3 desiredPos;
	private Transform thisTransform;

	// Use this for initialization
	void Start ()
	{
		thisTransform = this.transform;
	}

	void FixedUpdate ()
	{
		desiredPos = target.position - target.forward * distance + target.up * height;
		thisTransform.position = Vector3.Lerp (thisTransform.position, desiredPos, smoothSpeed * Time.deltaTime);
	}
	// Update is called once per frame
	void LateUpdate ()
	{
		
		thisTransform.LookAt (target, target.up);
	}
}
