using UnityEngine;
using System.Collections;

// ATTACH TO GAME CONTROLLER 

public class CameraZoom : MonoBehaviour {

	// GET THESE GAMEOBJECTS/COMPONENTS
	public Camera mainCam; 
	public Camera roomCam; 

	// CAMERA SIZES TO SWITCH TO WHEN ZOOM IN/OUT
	public float sizeToSeePlayer;	// following the player 
	public float sizeToSeeShip; 	// zoom out once
	public float sizeToSeeSpace; 	// zoom out twice
	public float sizeInc;

	// to switch ship views
	public GameObject shipOut; 

	// WHICH CAMERA VIEW ARE WE CURRENTLY USING
	public enum camView { player, ship, space }; 
	private int currentState; 	// will take one of the enum values/indeces
	private int previousState; 
	private Vector3 mainCamPosition; 

	void Start () 
	{
		mainCamPosition = mainCam.transform.position; 
	}

	void Update () 
	{
		switchCamera (); 
		camZoom (); 

		if (Input.GetKeyDown(KeyCode.R))
		{
			Application.LoadLevel ("TitleScreen");
		}
	}	

	/* --------------------------------------------------------------------------------------------------------------------------
	 * NO ARGS. NO RETURN. 
	 * (1) if press Q key, zoom in
	 * (2) if press E key, zoom out
	 * -------------------------------------------------------------------------------------------------------------------------- */

	private void switchCamera ()
	{

		if (Input.GetKeyDown (KeyCode.Q))
		{
			mainCam.transform.position = roomCam.transform.position; 
			if (roomCam.orthographicSize >= 5.0f)
			{
				currentState = (int) camView.ship; 
			}
			else 
			{
				currentState = (int) camView.player; 
			}
		}

		if (Input.GetKeyDown (KeyCode.E))
		{
			if (currentState == (int) camView.space)
			{
				mainCam.transform.position = roomCam.transform.position; 
				if (roomCam.orthographicSize >= 5.0f)
				{
					currentState = (int) camView.ship; 
				}
				else 
				{
					currentState = (int) camView.player; 
				}
			}
			else 
			{
				mainCam.transform.position = mainCamPosition;
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

			// (1) switching to player view (zoom in) 
			// *** SWITCH TO GUI CAMERA IF ZOOMED IN I.E. ABOUT TO INTERACT WITH CAPTAIN/ROOM
			case (int) camView.player:
				shipOut.SetActive (false);
				if (mainCam.orthographicSize <= sizeToSeePlayer)
				{
					mainCam.orthographicSize = sizeToSeePlayer; 
					mainCam.enabled = false;
					roomCam.enabled = true; 
					previousState = (int) camView.player;
				}
				else 
				{
					mainCam.orthographicSize -= sizeInc; 
				} 
				break;
			
			// (2) switching to ship view (must check if zooming out, if previously player view, or in, if previously space view)
			case (int) camView.ship:
				shipOut.SetActive (false); 
				mainCam.enabled = true;
				roomCam.enabled = false; 
				if (previousState == (int) camView.player)	// zoom out
				{
					if (mainCam.orthographicSize >= sizeToSeeShip)
					{
						mainCam.orthographicSize = sizeToSeeShip; 
						previousState = (int) camView.ship; 
					}
					else 
					{
						mainCam.orthographicSize += sizeInc; 
					}
				}
				else if (previousState == (int) camView.space)	// zoom in
				{
					if (mainCam.orthographicSize <= sizeToSeeShip)
					{
						mainCam.orthographicSize = sizeToSeeShip; 
						previousState = (int) camView.ship; 
					}
					else 
					{
						mainCam.orthographicSize -= sizeInc; 
					}
				}
				break;

			// (3) switching to space view (zoom out)
			case (int) camView.space: 
				shipOut.SetActive (true); 
				mainCam.enabled = true;
				roomCam.enabled = false; 
				if (mainCam.orthographicSize >= sizeToSeeSpace)
				{
					mainCam.orthographicSize = sizeToSeeSpace; 
					previousState = (int) camView.space; 
				}
				else 
				{
					mainCam.orthographicSize += sizeInc; 
				}
				break;

			default:
				break; 
			}
		}
	}
}
