using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour
{

	public GameObject joinPanel;
	public GameObject scorePanel;
	public Text scoreText;

	// Use this for initialization
	void Start ()
	{
		SetGUIVisibility ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void SetScore (int score)
	{
		scoreText.text = score.ToString ();
	}

	void SetGUIVisibility ()
	{
		if (GameManager.isPlaying == true) {
			scorePanel.SetActive (true);
			joinPanel.SetActive (false);
		} else {
			joinPanel.SetActive (true);
			scorePanel.SetActive (false);
		}
	}
}
