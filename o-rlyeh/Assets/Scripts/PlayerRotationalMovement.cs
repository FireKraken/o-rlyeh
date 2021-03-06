﻿using UnityEngine;
using System.Collections;

// ZERO GRAVITY IN THE SHIP - FLOAT AROUND USING WASD

public class PlayerRotationalMovement : MonoBehaviour {
	
	// movement stuff
	public Rigidbody rb; 
	//public float turnSpeed = 0.1f;	// if want some rotation?
	public float speed; 
	public float turnSpeed; 
	
	// sprite/animation switching depending on direction stuff
	public Animator anim; 
	private enum direction { idle, up, down, right, left }; 
	private int currentDirection; 
	
	// Use this for initialization
	void Start () 
	{
	}
	
	void Update()
	{
		
	}
	
	void FixedUpdate ()
	{
		playerMove (); 
		switchAnimation (currentDirection); 
	}
	
	/* --------------------------------------------------------------------------------------------------------------------------
	 * NO ARGS. NO RETURN. 
	 * (1) depending on key pressed (WASD), add a force to push character in the appropriate direction
	 * (2) change the current direction the player is 'facing' to change the animation 
	 * (3) if no button is pressed, current direction is idle => idle sprite
	 * -------------------------------------------------------------------------------------------------------------------------- */
	
	private void playerMove()
	{
		
		if (Input.GetKey (KeyCode.W)) 
		{
			rb.AddRelativeForce (Vector3.up * speed);
			currentDirection = (int)direction.up;
		}

		if (Input.GetKey (KeyCode.S)) 
		{
			rb.AddRelativeForce (Vector3.down * speed); 
			currentDirection = (int)direction.down;
		}
		
		if (Input.GetKey (KeyCode.A)) 
		{
			rigidbody.AddRelativeTorque (0, 0, turnSpeed);
			//currentDirection = (int)direction.left;
		}
		
		if (Input.GetKey (KeyCode.D)) 
		{
			rigidbody.AddRelativeTorque (0, 0, -turnSpeed);
			//currentDirection = (int)direction.right;
		}
		
		if (!Input.anyKey)
		{
			currentDirection = (int) direction.idle;
		}
	}
	
	/* --------------------------------------------------------------------------------------------------------------------------
	 * ARG: current direction of the player movement 
	 * (1) check inputted current direction - see which index in enum it corresponds to
	 * (2) set that as the "direction" integer value for the sprite animation
	 * -------------------------------------------------------------------------------------------------------------------------- */
	
	private void switchAnimation (int dir)
	{
		switch (dir) 
		{
		case (int) direction.idle:
			anim.SetInteger("direction", (int) direction.idle);
			break;
			
		case (int) direction.up:
			anim.SetInteger("direction", (int) direction.up);
			break;
			
		case (int) direction.down:
			anim.SetInteger("direction", (int) direction.down);
			break;
			
		case (int) direction.right:
			anim.SetInteger("direction", (int) direction.right);
			break;
			
		case (int) direction.left:
			anim.SetInteger("direction", (int) direction.left);
			break;
			
		default:
			break; 
		}
	}
}
