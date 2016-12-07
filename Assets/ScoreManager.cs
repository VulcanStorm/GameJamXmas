using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

	public static ScoreManager singleton;

	public int[] scoresToAdd;
	public int[] totalScores;
	public float[] timeSinceLastScore;

	public Text[] playerScoreTexts;

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
		if (GameManager.isPlaying) {
			float currentTime = Time.time;
			for (int i = 0; i < scoresToAdd.Length; i++) {
				if (scoresToAdd [i] < totalScores [i]) {
					// always lerp the score over a constant time
					float timeDiff = currentTime - timeSinceLastScore [i];
					scoresToAdd [i] = (int)Mathf.Lerp (scoresToAdd [i], totalScores [i], timeDiff);
				}
			}
		}
	}

	public void AddScoreForPlayer (int controllerNum, int score)
	{
		// player num is controller num -1
		totalScores [controllerNum - 1] += score;
		timeSinceLastScore [controllerNum - 1] = Time.time;
	}
}
