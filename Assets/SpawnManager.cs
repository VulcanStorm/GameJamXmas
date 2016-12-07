using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{

	public static SpawnManager singleton;

	public Transform[] playerSpawns;

	public GameObject playerPrefab;
	public GameObject cameraPrefab;


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

	public void SpawnAllPlayers (int numPlayers)
	{
		Rect[] cameraRects;
		if (numPlayers == 1) {
			cameraRects = new Rect[1];
			cameraRects [0] = new Rect (0, 0, 1, 1);
		} else if (numPlayers == 2) {
			cameraRects = new Rect[2];
			cameraRects [0] = new Rect (0, 0, 0.5f, 1);
			cameraRects [1] = new Rect (0.5f, 0, 1, 1);
		} else {
			cameraRects = new Rect[4];
			cameraRects [0] = new Rect (0, 0.5f, 0.5f, 0.5f);
			cameraRects [1] = new Rect (0.5f, 0.5f, 0.5f, 0.5f);
			cameraRects [2] = new Rect (0, 0, 0.5f, 0.5f);
			cameraRects [3] = new Rect (0.5f, 0, 0.5f, 0.5f);
		}

		for (int i = 0; i < numPlayers; i++) {
			Transform spawn = GetSpawnPointForPlayer (i);
			GameObject playerObj = (GameObject)Instantiate (playerPrefab, spawn.position, spawn.rotation);
			SleighController ctrl = playerObj.GetComponent<SleighController> ();
			// set the controller number so that the inputs are correct
			ctrl.controllerNumber = PlayerManager.GetControllerNumForPlayerNum (i + 1);
			// create camera
			GameObject cameraObj = (GameObject)Instantiate (cameraPrefab, spawn.position, spawn.rotation);
			CameraFollow camScript = cameraObj.GetComponent<CameraFollow> ();
			// set camera target and view Rect
			camScript.SetCameraTarget (ctrl.transform);
			camScript.SetCameraViewRect (cameraRects [i]);
			// spawn the camera here too

		}
	}

	public void SpawnCamerasForPlayers (int num)
	{
		// work out the camera rects?

	}

	public Transform GetSpawnPointForPlayer (int player)
	{
		return playerSpawns [player];
	}
}
