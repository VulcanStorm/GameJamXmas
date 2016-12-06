using UnityEngine;
using System.Collections;

[System.Serializable]
public sealed class XBoxCtrlInputs
{
	private int controllerNumber;

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

	public XBoxCtrlInputs (int playerNum)
	{
		controllerNumber = playerNum;
	}

	// Update is called once per frame
	public void UpdateController ()
	{
		// update our input
		switch (controllerNumber) {
		case 1:
			GetPlayer1Input ();
			break;
		case 2:
			GetPlayer2Input ();
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
		// get the input axes
		leftStickX = Input.GetAxisRaw ("Joy1_X");
		leftStickY = Input.GetAxisRaw ("Joy1_Y");
		rightStickX = Input.GetAxisRaw ("Joy1_X2");
		rightStickY = Input.GetAxisRaw ("Joy1_Y2");
		rightTrigger = Input.GetAxisRaw ("Joy1_Fire1");
		leftTrigger = Input.GetAxisRaw ("Joy1_Fire2");

		aButton = Input.GetKey (KeyCode.Joystick1Button0);
		bButton = Input.GetKey (KeyCode.Joystick1Button1);
		xButton = Input.GetKey (KeyCode.Joystick1Button2);
		yButton = Input.GetKey (KeyCode.Joystick1Button3);
		leftBumper = Input.GetKey (KeyCode.Joystick1Button4);
		rightBumper = Input.GetKey (KeyCode.Joystick1Button5);
		selectButton = Input.GetKey (KeyCode.Joystick1Button6);
		startButton = Input.GetKey (KeyCode.Joystick1Button7);
		leftStickPressed = Input.GetKey (KeyCode.Joystick1Button8);
		rightStickPressed = Input.GetKey (KeyCode.Joystick1Button9);

	}

	private void GetPlayer2Input ()
	{
		// get the input axes
		leftStickX = Input.GetAxisRaw ("Joy2_X");
		leftStickY = Input.GetAxisRaw ("Joy2_Y");
		rightStickX = Input.GetAxisRaw ("Joy2_X2");
		rightStickY = Input.GetAxisRaw ("Joy2_Y2");
		rightTrigger = Input.GetAxisRaw ("Joy2_Fire1");
		leftTrigger = Input.GetAxisRaw ("Joy2_Fire2");

		aButton = Input.GetKey (KeyCode.Joystick2Button0);
		bButton = Input.GetKey (KeyCode.Joystick2Button1);
		xButton = Input.GetKey (KeyCode.Joystick2Button2);
		yButton = Input.GetKey (KeyCode.Joystick2Button3);
		leftBumper = Input.GetKey (KeyCode.Joystick2Button4);
		rightBumper = Input.GetKey (KeyCode.Joystick2Button5);
		selectButton = Input.GetKey (KeyCode.Joystick2Button6);
		startButton = Input.GetKey (KeyCode.Joystick2Button7);
		leftStickPressed = Input.GetKey (KeyCode.Joystick2Button8);
		rightStickPressed = Input.GetKey (KeyCode.Joystick2Button9);
	}
	// TODO make functions for the other 2 players.
}


