using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class ElfManager : MonoBehaviour
{

	public GameObject superBadElfPrefab;
	public GameObject badElfPrefab;
	public GameObject goodElfPrefab;

	public ElfSpawnPoint[] elfSpawns;
	public int maxElves = 150;
	public int maxGoodElves = 50;
	public int elfCount;
	public int goodElfCount;
	public static ElfManager singleton;
	Transform thisTransform;
	public List<Elf> elfList = new List<Elf> ();

	int nextElfIndex;

	private LinkedList<int> unusedIDs = new LinkedList<int> ();

	public LayerMask elfWallLayers;

	public int RegisterElf (Elf elf)
	{
		int newId = -1;
		// check if there's a free ID
		if (unusedIDs.Count > 0) {
			newId = unusedIDs.First.Value;
			elfList [newId] = elf;
		} else {
			elfList.Add (elf);
			newId = elfList.Count - 1;
		}

		return newId;
	}

	public void UnregisterElf (int id, ElfType typ)
	{
		if (typ == ElfType.good) {
			goodElfCount -= 1;
		}
		elfCount -= 1;
		unusedIDs.AddLast (id);
		elfList [id] = null;
	}


	void Awake ()
	{
		singleton = this;
		thisTransform = this.transform;
		thisTransform.position = Vector3.zero;
	}

	void OnDestroy ()
	{
		singleton = null;
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.isPlaying == true) {
			for (int i = 0; i < elfSpawns.Length; i++) {
				if (elfCount < maxElves) {
					if (elfSpawns [i].canSpawn == true) {
						//print (i + " can spawn an elf");
						// get the spawn info
						Vector3 spawnPos = elfSpawns [i].GetSpawnPoint ();
						Quaternion spawnRotation = elfSpawns [i].GetSpawnRotation ();
						// reset the spawn timer
						elfSpawns [i].ResetSpawnTimer ();
						// pick a random elf to go here
						float elfNum = Random.Range (0, 1.0f);
						GameObject newElf;
						if (elfNum < 0.1f) {
							newElf = (GameObject)Instantiate (superBadElfPrefab, spawnPos, spawnRotation);
						} else if (elfNum > 0.75f) {
							newElf = (GameObject)Instantiate (goodElfPrefab, spawnPos, spawnRotation);
							goodElfCount += 1;
						} else {
							newElf = (GameObject)Instantiate (badElfPrefab, spawnPos, spawnRotation);
						}
						elfCount += 1;
						// parent the elf to this object
						newElf.transform.SetParent (thisTransform);
						Elf elfScript = newElf.GetComponent<Elf> ();
						elfScript.SetDesiredDirection (spawnRotation * Vector3.forward);
					}
				}
			}
		}
	}

	void FixedUpdate ()
	{
		for (int i = 0; i < 5; i++) {
			if (elfList.Count > 0) {
				nextElfIndex++;
				if (nextElfIndex >= elfList.Count) {
					nextElfIndex = 0;
				}

				if (elfList [nextElfIndex] != null) {
					elfList [nextElfIndex].CheckForWalls (elfWallLayers);
				}
			}
		}
	}
}
