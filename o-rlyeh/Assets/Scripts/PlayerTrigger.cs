using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour 
{
	// CAMERA
	public Camera mainCam;
	public SpriteFade sf; 
	
	// ZOOM IN/OUT STUFF
	public float camSizeBridge = 1.5f;
	public float camSizeOthers = 1.25f; 
	public float camSizeExit = 5.0f;

	public Vector3 curPos; 
	private float t;

	// LERP STUFF
	public Vector3 newPos;
	public float newSize;
	public float smooth = 0.5f;

	// Checks for whether a dialogue has been completed
	public bool captDialogue1 = true;
	public bool pugDialogue1 = false;
	public bool captDialogue2 = false;
	public bool pugDialogue2 = false;

	// Prevent player movement when action is paused
	public bool pauseAction = false;
	public bool promptVisible = false;
	public GameObject buttonPrompt;

	// Use this for initialization
	void Start () 
	{		
		newPos = mainCam.transform.position;
		newSize = mainCam.orthographicSize; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (pauseAction) 
		{
			// Pause the action
			rigidbody2D.isKinematic = true;
			// mainCam.orthographicSize = 5.0f;
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

	void FixedUpdate(){
		if (!GameObject.Find ("GameController").GetComponent<CameraZoom>().lerping){
			mainCam.orthographicSize = Mathf.Lerp (mainCam.orthographicSize, newSize, smooth * Time.deltaTime);
			mainCam.transform.position = Vector3.Slerp (mainCam.transform.position, newPos, smooth * Time.deltaTime);
		}
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		curPos = new Vector3 (mainCam.transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z);
		
		if (other.tag == "MessHall")
		{
			newSize = 1.25f;
			newPos = new Vector3 (-0.575f, 0.175f, curPos.z);
			Debug.Log ("Entered mess hall.");
		}
		else if (other.tag == "EngineRoom")
		{
			newSize = 1.25f;
			newPos = new Vector3 (-0.465f, -2.175f, curPos.z);
			Debug.Log ("Entered engine room.");
		}
		else if (other.tag == "CrewQuarters")
		{
			newSize = 1.25f;
			newPos = new Vector3 (-5.0f, 0.175f, curPos.z);
			Debug.Log ("Entered crew quarters.");
		}
		else if (other.tag == "ObservationDeck")
		{
			newSize = 1.25f;
			newPos = new Vector3 (0.0f, 2.5f, curPos.z);
			Debug.Log ("Entered observation deck.");
		}
		else if (other.tag == "Bridge")
		{
			newSize = 1.5f;
			newPos = new Vector3 (4.275f, 0.1f, curPos.z);
			Debug.Log ("Entered bridge.");
		}
		curPos = mainCam.transform.position; 
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.tag == "CaptainObjective" && (captDialogue1 || captDialogue2))
		{
			Debug.Log ("Entered Captain's conversation trigger.");
			promptVisible = true;

			if (Input.GetKeyDown (KeyCode.T))
			{
				pauseAction = true;
				promptVisible = false;
				captDialogue1 = false;
				sf.dialogueStarting = true;
			}
		}
	}
	
	void OnTriggerExit2D (Collider2D other)
	{
		curPos = new Vector3 (mainCam.transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z);
		
		if (other.tag == "MessHall" || other.tag == "EngineRoom" || other.tag == "CrewQuarters" || other.tag == "ObservationDeck" || other.tag == "Bridge")
		{
			newSize = 5.0f;
			newPos = new Vector3 (-0.575f, 0.175f, curPos.z);
			Debug.Log ("Moving through the ship.");
		}
		curPos = mainCam.transform.position; 

		if (other.tag == "CaptainObjective" && (captDialogue1 || captDialogue2))
		{
			promptVisible = false;
		}
	}
}