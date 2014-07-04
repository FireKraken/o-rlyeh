﻿using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour 
{
	// CAMERA
	public Camera mainCam;
	public SpriteFade sf; 
	
	// TIME STUFF
	private float startTime;
	public float duration = 1.0f;
	
	// ZOOM IN/OUT STUFF
	public float camSizeBridge = 1.5f;
	public float camSizeOthers = 1.25f; 
	public float camSizeExit = 5.0f;

	public Vector3 curPos; 
	private float t;

	// Checks for whether a dialogue has been completed
	private bool captDialogue0 = false;
	private bool pugDialogue1 = false;
	private bool captDialogue2 = false;
	private bool pugDialogue3 = false;

	// Prevent player movement when action is paused
	public bool pauseAction = false;
	private bool promptVisible = false;
	public GameObject buttonPrompt;

	// Use this for initialization
	void Start () 
	{		
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (pauseAction) 
		{
			// Pause the action
			rigidbody2D.isKinematic = true;
			mainCam.orthographicSize = 5.0f;
		} 
		else 
		{
			// Un-pause the action
			rigidbody2D.isKinematic = false;
			rigidbody2D.WakeUp();
		}

		if (promptVisible)
		{
			buttonPrompt.renderer.enabled = true;
		}
		else
		{
			buttonPrompt.renderer.enabled = false;
		}
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		float t = (Time.time - startTime / duration);
		curPos = new Vector3 (mainCam.transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z);
		
		if (other.tag == "MessHall")
		{
			mainCam.orthographicSize = 1.25f;
			mainCam.transform.position = Vector3.Slerp (curPos, new Vector3 (-0.575f, 0.175f, curPos.z), t);
			Debug.Log ("Entered mess hall.");
		}
		else if (other.tag == "EngineRoom")
		{
			mainCam.orthographicSize = 1.25f;
			mainCam.transform.position = Vector3.Slerp (curPos, new Vector3 (-0.465f, -2.175f, curPos.z), t);
			Debug.Log ("Entered engine room.");
		}
		else if (other.tag == "CrewQuarters")
		{
			mainCam.orthographicSize = 1.25f;
			mainCam.transform.position = Vector3.Slerp (curPos, new Vector3 (-5.0f, 0.175f, curPos.z), t);
			Debug.Log ("Entered crew quarters.");
		}
		else if (other.tag == "ObservationDeck")
		{
			mainCam.orthographicSize = 1.25f;
			mainCam.transform.position = Vector3.Slerp (curPos, new Vector3 (0.0f, 2.5f, curPos.z), t);
			Debug.Log ("Entered observation deck.");
		}
		else if (other.tag == "Bridge")
		{
			mainCam.orthographicSize = 1.5f;
			mainCam.transform.position = Vector3.Slerp (curPos, new Vector3 (4.275f, 0.1f, curPos.z), t);
			Debug.Log ("Entered bridge.");
		}
		curPos = mainCam.transform.position; 
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.tag == "CaptainObjective")
		{
			Debug.Log ("Entered Captain's conversation trigger.");
			promptVisible = true;

			if (Input.GetKeyDown (KeyCode.T))
			{
				pauseAction = true;
				promptVisible = false;
				sf.dialogueStarting = true;
			}
		}
	}
	
	void OnTriggerExit2D (Collider2D other)
	{
		float t = (Time.time - startTime / duration);
		curPos = new Vector3 (mainCam.transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z);
		
		if (other.tag == "MessHall" || other.tag == "EngineRoom" || other.tag == "CrewQuarters" || other.tag == "ObservationDeck" || other.tag == "Bridge")
		{
			mainCam.orthographicSize = 5.0f;
			mainCam.transform.position = Vector3.Slerp (curPos, new Vector3 (-0.575f, 0.175f, curPos.z), t);
			Debug.Log ("Moving through the ship.");
		}
		curPos = mainCam.transform.position; 

		if (other.tag == "CaptainObjective")
		{
			promptVisible = false;
		}
	}
}