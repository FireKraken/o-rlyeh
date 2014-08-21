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
	private Vector3 mainCamPosition; 
	

	void Start () 
	{
		shipOut = GameObject.Find ("ShipOuter");

		lerping = false;
		mainCamPosition = mainCam.transform.position; 

		currentState = (int) camView.player;

		newSize = sizeToSeeSpace;
	}

	void Update () 
	{
		switchOrtho (); 

		if (Input.GetKeyDown(KeyCode.R))
		{
			Application.LoadLevel ("TitleScreen");
		}
	}	

	void FixedUpdate(){
		if (lerping){
			mainCam.transform.position = Vector3.Lerp (mainCam.transform.position, mainCamPosition, smooth*Time.deltaTime);
			mainCam.orthographicSize = Mathf.Lerp (mainCam.orthographicSize, newSize, smooth*Time.deltaTime);
		}
	}

	/* --------------------------------------------------------------------------------------------------------------------------
	 * SWITCH CAMERA
	 * (1) if press Q key, zoom in
	 * (2) if press E key, zoom out
	 * -------------------------------------------------------------------------------------------------------------------------- */

	private void switchOrtho ()
	{
		if (Input.GetKeyDown (KeyCode.Z))
		{
			if (currentState == (int) camView.space)
			{
				lerping = false;
				shipOut.GetComponent<SpriteRenderer>().enabled = false; 
				currentState = (int) camView.ship;
			}
			else 
			{
				lerping = true;
				shipOut.GetComponent<SpriteRenderer>().enabled = true; 
				currentState = (int) camView.space; 
			}
			AudioManager.ins.SendMessage ("PlayClip"); 
		}
	}
}
