using UnityEngine;
using System.Collections;

public abstract class FourPCamera : MonoBehaviour
{
	public int playerNumber;

	public abstract void SetCameraViewRect (Rect rect);

	public abstract void SetCameraTarget (Transform t);

}
