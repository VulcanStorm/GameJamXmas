using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{

	public static PlayerManager singleton;

	public static int playerCount = -1;

	public static bool[] activePlayers = new bool[4];
	// maps player number to controller number
	// e.g. controller number is at playerNumber index
	public int[] playerControllerMapping = new int[4];
	public List<Transform> playerList = new List<Transform> ();

	public int[] scores;

	public PlayerGUI[] playerGUIs;

	public List<FourPCamera> playerCameras;

	public Transform GetNearestSanta (Vector3 pos, out float outDist)
	{
		float dist = 10000;
		float newDist;
		Transform returnTrans = null;
		int index = 0;
		for (int i = 0; i < playerList.Count; i++) {
			newDist = (playerList [i].position - pos).sqrMagnitude;
			if (newDist < dist) {
				dist = newDist;
				index = i;
				returnTrans = playerList [i];
			}

		}
		outDist = dist;
		return returnTrans;
	}

	public void AddScoreForController (int playerNum, int score)
	{
		int scoreIndex = GetControllerNumForPlayerNum (playerNum);
		scores [scoreIndex - 1] += score;
	}

	public void AddNewSleigh (SleighController sleigh)
	{
		playerList.Add (sleigh.transform);
	}

	void Awake ()
	{
		singleton = this;
		for (int i = 0; i < 4; i++) {
			playerControllerMapping [i] = -1;
		}
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameManager.isPlaying == false) {
			if (ControllerInputManager.GetInputsForControllerNumber (1).aButton) {
				// has player 1
				activePlayers [0] = true;
			}
			// player 1 can't unregister...

			if (ControllerInputManager.GetInputsForControllerNumber (2).aButton) {
				// has player 2
				activePlayers [1] = true;
			} else if (ControllerInputManager.GetInputsForControllerNumber (2).bButton) {
				// don't have player 4
				activePlayers [1] = false;
			}

			if (ControllerInputManager.GetInputsForControllerNumber (3).aButton) {
				// has player 3
				activePlayers [2] = true;
			} else if (ControllerInputManager.GetInputsForControllerNumber (3).bButton) {
				// don't have player 4
				activePlayers [2] = false;
			}

			if (ControllerInputManager.GetInputsForControllerNumber (4).aButton) {
				// has player 4
				activePlayers [3] = true;
			} else if (ControllerInputManager.GetInputsForControllerNumber (4).bButton) {
				// don't have player 4
				activePlayers [3] = false;
			}

			// TODO rewrite this to wait for all customisation
			// if we have player 1... which we must do... then do we want to start?
			if (activePlayers [0] == true) {
				if (ControllerInputManager.GetInputsForControllerNumber (1).startButton) {
					// how many players?
					playerCount = 0;
					for (int i = 0; i < activePlayers.Length; i++) {
						if (activePlayers [i]) {
							playerControllerMapping [playerCount] = i + 1;
							playerCount++;
						}
					}
					// start game
					// spawn players and cameras
					SpawnManager.singleton.SpawnAllPlayers (playerCount);
					// now we're playing
					GameManager.isPlaying = true;
					// refresh the guis now the game has started
					for (int i = 0; i < playerGUIs.Length; i++) {
						playerGUIs [i].SetGUIVisibility ();
					}
					RoundTimer.singleton.StartRound ();
				}
			}
		}
	}

	public static int GetControllerNumForPlayerNum (int plNum)
	{
		return singleton.playerControllerMapping [plNum - 1];
	}
}
