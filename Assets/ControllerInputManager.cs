using UnityEngine;
using System.Collections;

public class ControllerInputManager : MonoBehaviour
{

	public static XBoxCtrlInputs player1Inputs = new XBoxCtrlInputs (1);
	public static XBoxCtrlInputs player2Inputs = new XBoxCtrlInputs (2);
	public static XBoxCtrlInputs player3Inputs = new XBoxCtrlInputs (3);
	public static XBoxCtrlInputs player4Inputs = new XBoxCtrlInputs (4);


	public static XBoxCtrlInputs GetInputsForControllerNumber (int num)
	{
		if (num == 1) {
			return player1Inputs;
		} else if (num == 2) {
			return player2Inputs;
		} else if (num == 3) {
			return player3Inputs;
		} else if (num == 4) {
			return player4Inputs;
		} else {
			return null;
		}
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		player1Inputs.UpdateController ();	
		player2Inputs.UpdateController ();
		player3Inputs.UpdateController ();
		player4Inputs.UpdateController ();
	}
}
