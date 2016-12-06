using UnityEngine;
using System.Collections;

public class Elf : MonoBehaviour
{
	public int elfId = -1;

	Rigidbody thisRigidbody;
	Transform thisTransform;
	public Transform animTransform;
	Vector3 santaDir = Vector3.zero;
	Vector3 moveDir = Vector3.forward;
	public float runSpeed = 2.75f;
	public float walkSpeed = 1;
	public float distToSanta;

	Vector3 desiredDirection;

	public int health = 50;
	public int score = 100;

	public float sqrDistanceToFlee = 200;
	Transform nearestSanta;

	public ElfType elfType = ElfType.bad;
	private static RaycastHit hit;

	// Use this for initialization
	void Start ()
	{
		//desiredDirection = (new Vector3 (Random.Range (-1, 1), 0, Random.Range (-1, 1))).normalized;
		thisTransform = this.transform;
		thisRigidbody = this.GetComponent<Rigidbody> ();
		// register the elf
		elfId = ElfManager.singleton.RegisterElf (this);
	}

	void OnDestroy ()
	{
		// clear variables
		nearestSanta = null;
		thisTransform = null;
		thisRigidbody = null;
		if (ElfManager.singleton)
			ElfManager.singleton.UnregisterElf (elfId, elfType);
	}


	public void SetDesiredDirection (Vector3 dir)
	{
		desiredDirection = dir;
	}

	public void CheckForWalls (LayerMask layers)
	{
		if (Physics.Raycast (thisTransform.position, moveDir, out hit, 3, layers)) {

			desiredDirection = (desiredDirection + hit.normal).normalized;
		}
	}

	public void TakeDamage (int player, int dmg)
	{
		health -= dmg;
		if (health <= 0) {
			KillElf (player);
		}
	}

	private void KillElf (int player)
	{
		EffectsManager.singleton.SpawnBloodSplatter (thisTransform.position - Vector3.up, moveDir);

		PlayerManager.singleton.AddScoreForPlayer (player, score);

		Destroy (gameObject);
	}

	void FixedUpdate ()
	{
		// run randonly unless santa is near
		GetDistToNearestSanta ();

		if (distToSanta < sqrDistanceToFlee) {
			if (nearestSanta != null) {
				// work out direction to santa
				santaDir = (thisTransform.position - nearestSanta.position).normalized;
				if (elfType != ElfType.good) {
					
					// run behind santa if we're infront of him
					if (Vector3.Dot (santaDir, nearestSanta.forward) > 0) {
						santaDir += -nearestSanta.forward;
					}
					moveDir = santaDir.normalized;
				} else {
					moveDir = santaDir;
				}

				desiredDirection = santaDir;
			}

			// now just run in this direction
			moveDir *= runSpeed;

		} else {
			// just move forwards and turn randomly
			moveDir = thisTransform.InverseTransformDirection (desiredDirection) * walkSpeed;

		}
		moveDir.y = thisRigidbody.velocity.y;
		thisRigidbody.velocity = moveDir;
		// set the animation direction
		if (thisRigidbody.velocity != Vector3.zero) {
			animTransform.rotation = Quaternion.LookRotation (thisRigidbody.velocity, Vector3.up);
		}
	}

	void GetDistToNearestSanta ()
	{
		nearestSanta = PlayerManager.singleton.GetNearestSanta (thisTransform.position, out distToSanta);
	}
}
