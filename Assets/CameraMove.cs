using UnityEngine;
using System.Collections;
[RequireComponent (typeof (Camera))]

/// <summary>
/// Addes the ability to move and rotate the camera that this script is attached to.
/// </summary>
public class CameraMove : MonoBehaviour
{	
	void Awake()
	{
		// Saves the initial transform values for later resetting
		originPos = transform.position;
		originRot = transform.localEulerAngles;
		originScl = transform.localScale;
	}

	void Update ()
	{
		float Th = 0;	// Horizontal transform value
		float Tv = 0;	// Verticle transform value

		float Rx = 0;	// Rotation around the X-axis
		float Ry = 0;	// Rotation around the Y-axis

		if(Input.GetMouseButton(2))	// Checks if the left mouse button is being clicked
		{
			Th = speed * Input.GetAxis("Mouse X") * Time.deltaTime;	// Sets Th to the difference the horizontal movement of the mouse
			Tv = speed * Input.GetAxis("Mouse Y") * Time.deltaTime;	// Sets Tv to the difference the verticle movement of the mouse
		}

		if(Input.GetMouseButton(1))	// Checks if the right mouse button is being clicked
		{
			Rx = speed * 0.25f * Input.GetAxis("Mouse Y") * Time.deltaTime;	// Sets Rx to the difference the verticle movement of the mouse
			Ry = speed * 0.25f * Input.GetAxis("Mouse X") * Time.deltaTime;	// Sets Ry to the difference the horizontal movement of the mouse
		}

		if(Input.GetAxis("Mouse ScrollWheel") > 0)	// Checks if the mouse wheele is being rolled forward
			transform.position += transform.forward * speed * Time.deltaTime;	// Moves the camera forward

		if(Input.GetAxis("Mouse ScrollWheel") < 0)	// Checks if the mouse wheele is being rolled backward
			transform.position -= transform.forward * speed * Time.deltaTime;	// Moves the camera backward

		transform.Translate(-Th, -Tv, 0);									// Moves the camera: left, right, up, and down
		transform.RotateAround(transform.position, transform.right, -Rx);	// Rotates the camera to look up
		transform.RotateAround(transform.position, Vector3.up, Ry);			// Rotates the camera to look down

		/// Will reset the camera ti its initial position	//
		if(Input.GetKey(resetKey))							//
		{													//
			transform.position = originPos;					//
			transform.localEulerAngles = originRot;			//
			transform.localScale = originScl;				//
		}
	}


	public float speed;			// The speed the camera will move or rotate at
	public KeyCode resetKey;	// The key that will be used to reset the camera's transform

	Vector3 originPos;			// Initial position of the camera
	Vector3 originRot;			// Initial rotation of the camera
	Vector3 originScl;			// Initial scale of the camera
}