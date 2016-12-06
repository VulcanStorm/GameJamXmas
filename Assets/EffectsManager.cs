using UnityEngine;
using System.Collections;

public class EffectsManager : MonoBehaviour
{

	public GameObject bloodSplatterPrefab = null;

	public static EffectsManager singleton;

	void Awake ()
	{
		singleton = this;
	}

	public void SpawnBloodSplatter (Vector3 pos, Vector3 direction)
	{
		Instantiate (bloodSplatterPrefab, pos, Quaternion.LookRotation (direction, Vector3.up));
	}

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
