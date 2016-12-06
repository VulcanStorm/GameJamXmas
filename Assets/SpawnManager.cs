using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{

	public static SpawnManager singleton;

	public Transform[] playerSpawns;

	public GameObject playerPrefab;


	void Awake ()
	{
		singleton = this;
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void SpawnAllPlayers ()
	{

	}

	public Transform GetSpawnPointForPlayer (int player)
	{
		return playerSpawns [player];
	}
}
