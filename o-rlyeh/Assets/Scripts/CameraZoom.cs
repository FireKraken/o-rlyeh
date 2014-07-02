using UnityEngine;
using System.Collections;

// ATTACH TO GAME CONTROLLER 

public class CameraZoom : MonoBehaviour {

	// GET THESE GAMEOBJECTS/COMPONENTS
	public Camera cam; 
	public Transform target;	// the player's coordinates 

	// CAMERA SIZES TO SWITCH TO WHEN ZOOM IN/OUT
	public float sizeToSeePlayer;	// following the player 
	public float sizeToSeeShip; 	// zoom out once
	public float sizeToSeeSpace; 	// zoom out twice
	public float sizeInc;

	// WHICH CAMERA VIEW ARE WE CURRENTLY USING
	public enum camera { player, ship, space }; 
	public int currentState; 	// will take one of the enum values/indeces
	public int previousState; 
	
	void Start () 
	{
		
	}

	void Update () 
	{
		switchCamera (); 
		camZoom(); 
	}	

	/* --------------------------------------------------------------------------------------------------------------------------
	 * NO ARGS. NO RETURN. 
	 * in player view, camera follows the player around the ship
	 * -------------------------------------------------------------------------------------------------------------------------- */

	private void cameraFollow()
	{

	}	

	/* --------------------------------------------------------------------------------------------------------------------------
	 * NO ARGS. NO RETURN. 
	 * (1) if press Q key, zoom in
	 * (2) if press E key, zoom out
	 * -------------------------------------------------------------------------------------------------------------------------- */

	private void switchCamera()
	{
		if (Input.GetKeyDown (KeyCode.Q))
		{
			if (currentState == (int) camera.player)
			{
				currentState = (int) camera.space; 
			}
			else 
			{
				currentState--; 
			}
		}
		if (Input.GetKeyDown (KeyCode.E))
		{
			if (currentState == (int) camera.space)
			{
				currentState = (int) camera.player; 
			}
			else 
			{
				currentState++; 
			}
		}
	}

	/* --------------------------------------------------------------------------------------------------------------------------
	 * ARG: current camera state to know how far to zoom in/out
	 * (1) switch to player view: must zoom in regardless of previous state
	 * (2) switch to ship view: zoom OUT if previous is player view, IN if previous is space view
	 * (3) switch to space view: must zoom out regardless of previous state
	 * -------------------------------------------------------------------------------------------------------------------------- */

	private void camZoom()
	{
		// if the camera state has changed 
		if (currentState != previousState)
		{
			switch (currentState) 
			{

			// (1) switching to player view
			case (int) camera.player:
				if (cam.orthographicSize <= sizeToSeePlayer)
				{
					cam.orthographicSize = sizeToSeePlayer; 
				}
				else 
				{
					cam.orthographicSize -= sizeInc; 
				}
				previousState = (int) camera.player; 
				break;
			
			// (2) switching to ship view (must check if zooming out or in)
			case (int) camera.ship:
				if (previousState == (int) camera.player)
				{
					if (cam.orthographicSize >= sizeToSeeShip)
					{
						cam.orthographicSize = sizeToSeeShip; 
					}
					else 
					{
						cam.orthographicSize += sizeInc; 
					}
					previousState = (int) camera.player; 
				}
				else if (previousState == (int) camera.ship)
				{
					if (cam.orthographicSize <= sizeToSeeShip)
					{
						cam.orthographicSize = sizeToSeeShip; 
					}
					else 
					{
						cam.orthographicSize -= sizeInc; 
					}
					previousState = (int) camera.ship; 
				}
				break;

			// (3) switching to space view
			case (int) camera.space:
				if (cam.orthographicSize >= sizeToSeeSpace)
				{
					cam.orthographicSize = sizeToSeeSpace; 
				}
				else 
				{
					cam.orthographicSize += sizeInc; 
				}
				previousState = (int) camera.space; 
				break;

			default:
				break; 
			}
		}
	}
}
