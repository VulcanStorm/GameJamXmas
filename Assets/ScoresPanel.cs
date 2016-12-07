using UnityEngine;
using System.Collections;

public class ScoresPanel : MonoBehaviour
{

	public static ScoresPanel singleton;

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
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void ShowScores ()
	{
		gameObject.SetActive (true);
	}
}
