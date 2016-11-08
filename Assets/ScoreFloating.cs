using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScoreFloating : MonoBehaviour
{
	RectTransform thisRectTransform;
	Text displayText;
	public bool isShown = false;
	float timer = 0;
	float wobbleTimer = 0;
	bool doneFloat;

	static Quaternion leftRot = Quaternion.Euler (0, 0, 10);
	static Quaternion rightRot = Quaternion.Euler (0, 0, -10);
	Quaternion fromRot;
	Quaternion toRot;

	static float floatTime = 1.75f;
	static float wobbleTime = 0.25f;
	static float stopWobbleTime = 0.2f;
	static float zoomTime = 2.75f;

	public static Vector2 player1ScoreRect;
	public static Vector2 player2ScoreRect;
	public static Vector2 player3ScoreRect;
	public static Vector3 player4ScoreRect;
	Vector2 targetScoreRect;
	Vector2 floatPos;

	void Awake ()
	{
		thisRectTransform = GetComponent<RectTransform> ();
		thisRectTransform.rotation = leftRot;
		fromRot = leftRot;
		toRot = rightRot;
	}

	void Display (int scoreValue, int playerCorner)
	{
		displayText.text = scoreValue.ToString ();
		timer = 0;
		wobbleTimer = 0;
		thisRectTransform.rotation = fromRot;
		switch (playerCorner) {
		case 1:
			targetScoreRect = player1ScoreRect;
			break;
		case 2:
			targetScoreRect = player2ScoreRect;
			break;
		case 3:
			targetScoreRect = player3ScoreRect;
			break;
		case 4:
			targetScoreRect = player4ScoreRect;
			break;
		default:
			throw new UnassignedReferenceException ("This player corner does not exist " + playerCorner);
		}

	}

	void Update ()
	{
		if (isShown == true) {
			
			timer += Time.deltaTime;
			if (timer < floatTime) {
				wobbleTimer += Time.deltaTime;
				if (wobbleTimer > wobbleTime) {
					Quaternion temp = fromRot;
					fromRot = toRot;
					toRot = temp;
					wobbleTimer = 0;
				}
				thisRectTransform.rotation = Quaternion.Lerp (fromRot, toRot, (wobbleTimer / wobbleTime));
			} else {
				if (doneFloat = false) {
					doneFloat = true;
					timer = 0;
				}
					
				if (timer < zoomTime) {
					thisRectTransform.rotation = Quaternion.Lerp (thisRectTransform.rotation, Quaternion.identity, timer / zoomTime);
					thisRectTransform.position = Vector3.Lerp (floatPos, targetScoreRect, timer * timer / zoomTime);
				} else {
					// hide this score
				}
			}

		}
	}
}
