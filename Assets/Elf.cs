using UnityEngine;
using System.Collections;

public class Elf : MonoBehaviour
{
	public int elfId = -1;

	Rigidbody thisRigidbody;
	Transform thisTransform;
	public Transform animTransform;
	public Animator animController;
	Vector3 santaDir = Vector3.zero;
	Vector3 moveDir = Vector3.forward;
	public float runSpeed = 2.75f;
	public float walkSpeed = 1;
	public float distToSanta;

	Vector3 desiredDirection;
	bool doRun = false;
	bool lastRun = false;
	public int health = 50;
	public int score = 100;

	public float sqrDistanceToFlee = 200;
	Transform nearestSanta;


	public ElfType elfType = ElfType.bad;
	private static RaycastHit hit;
	Quaternion animRot;

	// Use this for initialization
	void Start ()
	{
		//desiredDirection = (new Vector3 (Random.Range (-1, 1), 0, Random.Range (-1, 1))).normalized;
		thisTransform = this.transform;
		thisRigidbody = this.GetComponent<Rigidbody> ();
		// set the anim params
		animController.SetBool ("Run", false);
		animController.SetFloat ("Speed", walkSpeed);
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
		if (Physics.Raycast (thisTransform.position, moveDir, out hit, 4, layers)) {

			desiredDirection = (desiredDirection + 2 * hit.normal).normalized;
		}
	}

	public void TakeDamage (int player, int dmg)
	{
		health -= dmg;
		if (health <= 0) {
			KillElf (player);
		}
	}

	private void KillElf (int controller)
	{
		EffectsManager.singleton.SpawnBloodSplatter (thisTransform.position - Vector3.up, moveDir);

		PlayerManager.singleton.AddScoreForController (controller, score);

		Destroy (gameObject);
	}

	void FixedUpdate ()
	{
		// run randonly unless santa is near
		GetDistToNearestSanta ();

		if (distToSanta < sqrDistanceToFlee) {
			doRun = true;
			if (nearestSanta != null) {
				// work out direction to santa
				santaDir = (thisTransform.position - nearestSanta.position).normalized;
				if (elfType == ElfType.superBad) {
					
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
			doRun = false;
			// just move forwards and turn randomly
			moveDir = thisTransform.InverseTransformDirection (desiredDirection) * walkSpeed;

		}
		if (doRun != lastRun) {
			animController.SetBool ("Run", doRun);
			lastRun = doRun;
			if (doRun == false) {
				animController.SetFloat ("Speed", walkSpeed);
			} else {
				animController.SetFloat ("Speed", runSpeed);
			}
		}
		moveDir.y = thisRigidbody.velocity.y;
		thisRigidbody.velocity = moveDir;
		// set the animation direction
		if (thisRigidbody.velocity != Vector3.zero) {
			animRot = Quaternion.LookRotation (thisRigidbody.velocity, Vector3.up);
			animTransform.rotation = Quaternion.Slerp (animTransform.rotation, animRot, Time.deltaTime * 10);
		}
	}

	void GetDistToNearestSanta ()
	{
		nearestSanta = PlayerManager.singleton.GetNearestSanta (thisTransform.position, out distToSanta);
	}
}
