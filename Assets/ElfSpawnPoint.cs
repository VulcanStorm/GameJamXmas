using UnityEngine;
using System.Collections;

public class ElfSpawnPoint : MonoBehaviour
{


	public float spawnTime = 2.33f;
	float spawnTimer;
	float distToSanta;
	public float spawnZoneRadius = 30;
	float spawnZoneRadiusSqr = 0;
	Transform thisTransform;

	public bool canSpawn = true;

	void Awake ()
	{
		thisTransform = this.transform;
		spawnZoneRadiusSqr = spawnZoneRadius * spawnZoneRadius;
	}

	// Use this for initialization
	void Start ()
	{
	
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position, spawnZoneRadius);
	}
	
	// Update is called once per frame
	void Update ()
	{
		PlayerManager.singleton.GetNearestSanta (thisTransform.position, out distToSanta);
		if (distToSanta < spawnZoneRadiusSqr) {
			canSpawn = false;
		} else if (spawnTimer > spawnTime) {
			canSpawn = true;
		} else {
			canSpawn = false;
		}
		spawnTimer += Time.deltaTime;
	}

	public void ResetSpawnTimer ()
	{
		spawnTimer = 0;
	}

	public Vector3 GetSpawnPoint ()
	{
		return thisTransform.position;
	}

	public Quaternion GetSpawnRotation ()
	{
		return thisTransform.rotation;
	}


}
