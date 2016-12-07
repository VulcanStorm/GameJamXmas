using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoundTimer : MonoBehaviour
{
	public static RoundTimer singleton;

	bool timerStarted = false;
	Text timerText;

	float roundTime;
	int minutesLeft;
	int secondsLeft;
	float roundTimer;

	string minutesString;
	string secondsString;

	void Awake ()
	{
		singleton = this;
		gameObject.SetActive (false);
		timerText = this.GetComponent<Text> ();
	}

	void OnDestroy ()
	{
		singleton = null;
	}

	// Use this for initialization
	public void StartRound ()
	{
		timerStarted = true;
		roundTime = 5;
		roundTimer = roundTime;
		gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (timerStarted == true) {
			roundTimer -= Time.deltaTime;
		}
		if (roundTimer < 0) {
			roundTimer = 0;
			timerStarted = false;
			// GAME OVER
			Time.timeScale = 0;
			// show scores
			ScoresPanel.singleton.ShowScores ();
		}

		minutesLeft = Mathf.FloorToInt (roundTimer / 60);
		secondsLeft = Mathf.FloorToInt (roundTimer % 60);
		if (minutesLeft < 10) {
			minutesString = "0" + minutesLeft.ToString ();
		} else {
			minutesString = minutesLeft.ToString ();
		}
		if (secondsLeft < 10) {
			secondsString = "0" + secondsLeft.ToString ();
		} else {
			secondsString = secondsLeft.ToString ();
		}
		timerText.text = minutesString + ":" + secondsString;
	}
}
