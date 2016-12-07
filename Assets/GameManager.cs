using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public static GameManager singleton;

	public static bool isPlaying = false;

	void Awake ()
	{
		singleton = this;
	}

	void OnDestroy ()
	{
		singleton = null;
	}

	// Use this for initialization
	void Start ()
	{
		
	}

	public static void StartGame ()
	{
		isPlaying = true;
	}

	// Update is called once per frame
	void Update ()
	{
	
	}

	public void GameOver ()
	{
		Camera.main.gameObject.SetActive (true);
	}
}
