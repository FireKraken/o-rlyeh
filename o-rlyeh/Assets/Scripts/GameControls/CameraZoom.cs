using UnityEngine;
using System.Collections;

// ATTACH TO GAME CONTROLLER 

public class CameraZoom : MonoBehaviour {

	// GET THESE GAMEOBJECTS/COMPONENTS
	public Camera mainCam; 
	public PlayerTrigger trigger;

	// CAMERA SIZES TO SWITCH TO WHEN ZOOM IN/OUT
	public float sizeToSeePlayer;	// following the player 
	public float sizeToSeeShip; 	// zoom out once
	public float sizeToSeeSpace; 	// zoom out twice
	public float sizeInc;
	private float newSize;

	public float smooth;
	public bool lerping; 
	public float lerpDelay;

	// to switch ship views
	public GameObject shipOut; 

	// WHICH CAMERA VIEW ARE WE CURRENTLY USING
	public enum camView { player, ship, space }; 
	public int currentState; 	// will take one of the enum values/indeces
	private int previousState; 
	private Vector3 mainCamPosition; 

	// SOUND STUFF
	public AudioClip[] clips; 

	void Start () 
	{
		lerping = false;
		mainCamPosition = mainCam.transform.position; 
		audio.clip = clips [0];
		audio.Play (); 

		currentState = (int) camView.player;
		previousState = (int) camView.player;

		newSize = mainCam.orthographicSize;
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

	void FixedUpdate(){
		mainCam.orthographicSize = Mathf.Lerp (mainCam.orthographicSize, newSize, smooth*Time.deltaTime);
	}

	/* --------------------------------------------------------------------------------------------------------------------------
	 * SWITCH CAMERA
	 * (1) if press Q key, zoom in
	 * (2) if press E key, zoom out
	 * -------------------------------------------------------------------------------------------------------------------------- */

	private void switchCamera ()
	{
		if (Input.GetKeyDown (KeyCode.Z))
		{
			if (currentState == (int) camView.space)
			{
				currentState = (int) camView.ship;
			}
			else 
			{
				currentState = (int) camView.space; 
			}
		}
	}

	/* --------------------------------------------------------------------------------------------------------------------------
	 * CAM ZOOM. ARG: current camera state to know how far to zoom in/out
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
				if (previousState == (int) camView.space)
				{
					audio.clip = clips [0];
					audio.Play (); 
				}
				shipOut.SetActive (false); 
				previousState = (int) camView.player;
				break;

			case (int) camView.ship:
				if (previousState == (int) camView.space)
				{
					audio.clip = clips [0];
					audio.Play (); 
				}
				if (mainCam.orthographicSize <= sizeToSeeShip)
				{
					mainCam.orthographicSize = sizeToSeeShip; 
					previousState = (int) camView.ship; 
					currentState = (int) camView.player; 
				}
				else 
				{
					mainCam.orthographicSize -= sizeInc; 
				}
				shipOut.SetActive (false); 
				break; 
			// (3) switching to space view (zoom out)
			case (int) camView.space: 
				if (previousState != (int) camView.space)
				{
					audio.clip = clips [1];
					audio.Play (); 
				}
				shipOut.SetActive (true); 
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
			lerping = true; 
			StartCoroutine (lerpCam());
		}
	}
	IEnumerator lerpCam(){
		yield return new WaitForSeconds (lerpDelay);
		lerping = false; 
	}
}
