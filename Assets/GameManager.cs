using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public static bool isPlaying = false;

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
}
