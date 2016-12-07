using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour
{
	public int playerIndex = -1;
	public GameObject joinPanel;
	public GameObject optionsPanel;
	public GameObject scorePanel;
	public Text scoreText;
	int lastScore = 0;
	int actualScore = 0;
	int displayScore = 0;
	float timeSinceLastScore = 0;
	float timeDiff;
	public bool lastActive;

	// Use this for initialization
	void Start ()
	{
		SetGUIVisibility ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// check if the game has started
		if (GameManager.isPlaying == true) {
			actualScore = PlayerManager.singleton.scores [playerIndex];
			if (lastScore != actualScore) {
				lastScore = actualScore;
				timeSinceLastScore = Time.time;
			}
			// always lerp the score over a constant time
			timeDiff = Time.time - timeSinceLastScore;
			if (timeDiff < 1.1f) {
				displayScore = Mathf.RoundToInt (Mathf.Lerp (displayScore, actualScore, timeDiff));
				scoreText.text = "Score: " + displayScore.ToString ();
			}
		} else {
			// still in menu
			if (lastActive != PlayerManager.activePlayers [playerIndex]) {
				lastActive = PlayerManager.activePlayers [playerIndex];
				SetGUIVisibility ();
			}
			// check if our player is active, do customisation here
			if (PlayerManager.activePlayers [playerIndex]) {
				
			}
		}
	}

	public void SetGUIVisibility ()
	{
		if (GameManager.isPlaying == true) {
			// check for non existant player controller mapping
			if (PlayerManager.singleton.playerControllerMapping [playerIndex] == -1) {
				Destroy (gameObject);
			}
			scorePanel.SetActive (true);
			joinPanel.SetActive (false);
			optionsPanel.SetActive (false);
		} else {
			if (PlayerManager.activePlayers [playerIndex] == false) {
				joinPanel.SetActive (true);
				scorePanel.SetActive (false);
				optionsPanel.SetActive (false);
			} else {
				joinPanel.SetActive (false);
				scorePanel.SetActive (false);
				optionsPanel.SetActive (true);
			}
		}
	}
}
