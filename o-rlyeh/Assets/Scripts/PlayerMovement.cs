using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float speed = 0.8f;
	public float turnSpeed = 0.15f;

	// Use this for initialization
	void Start () 
	{
		rigidbody.useGravity = false;
	}

	void FixedUpdate ()
	{
		// Pressing spacebar adds upward momentum.
		if (Input.GetButton ("Jump"))
		{
			rigidbody.AddRelativeForce (Vector3.up * speed);
		}

		// Press W or the up arrow key to turn upwards, and S or the down arrow key to turn downwards.
		rigidbody.AddRelativeTorque ((Input.GetAxis ("Vertical")) * turnSpeed, 0, 0);

		// Press A or the left arrow key to turn left, D or the right arrow key to turn right.
		rigidbody.AddRelativeTorque (0, 0, (Input.GetAxis ("Horizontal")) * turnSpeed);
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
