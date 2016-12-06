using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider))]
public abstract class SleighComponent : MonoBehaviour
{
	protected SleighController sleigh;

	public Collider coll;

	protected Transform thisTransform;

	void Awake ()
	{
		thisTransform = this.transform;
		coll = this.GetComponent<Collider> ();
	}

	void OnDestroy ()
	{
		sleigh = null;
	}

	public void SetSleigh (SleighController sleigh)
	{
		this.sleigh = sleigh;
	}

	public void SetAttachmentPoint (Transform point)
	{
		thisTransform.parent = point;
		thisTransform.localPosition = Vector3.zero;
		thisTransform.localRotation = Quaternion.identity;
	}

	public void RemoveAttachment ()
	{
		thisTransform = null;
		Destroy (gameObject);
	}

	public abstract void EnterCollision (Elf elf);

	public abstract void StayCollision (Elf elf);
}
