using UnityEngine;
using System.Collections;

public class XBoxCtrlInputs
{
	public int controllerNumber;

	public bool aButton;
	public bool bButton;
	public bool xButton;
	public bool yButton;

	public bool startButton;
	public bool selectButton;

	public float leftStickX;
	public float leftStickY;
	public bool leftStickPressed;
	public float rightStickX;
	public float rightStickY;
	public bool rightStickPressed;

	public float leftTrigger;
	public float rightTrigger;
	public bool leftBumper;
	public bool rightBumper;



	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void UpdateController ()
	{
		// update our input
		switch (controllerNumber) {
		case 1:

			break;
		case 2:

			break;
		case 3:

			break;

		case 4:

			break;

		default:
			throw new MissingReferenceException ("This controller index doesn't exist: " + controllerNumber.ToString ());
		}
	}

	private void GetPlayer1Input ()
	{
		// TODO fill this in, and make it work
		// get each button based off the joystick directly.
	}
	// TODO make functions for the other 3 players.
}


