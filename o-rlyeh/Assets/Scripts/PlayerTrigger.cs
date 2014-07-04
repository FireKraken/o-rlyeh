using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour 
{
	public float duration = 2.0f;

	public float camSizeBridge = 1.5f;
	public float camSizeOthers = 1.25f; 
	public float camSizeExit = 5.0f;

	private bool exitRoom;
	private bool enterRoom; 
	private Vector3 curPos; 
	private float t; 
	private enum rm { MessHall, EngineRoom, CrewQuarters, ObservationDeck, Bridge } ;
	private int currentRoom; 

	public float sizeInc;
	public float posInc; 

	public Camera roomCam;

	private float startTime;

	// Use this for initialization
	void Start () 
	{
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void FixedUpdate()
	{
		t = (Time.time - startTime) / duration;

		if (enterRoom) {
			enteringRoom (); 
		}

		if (exitRoom) {
			exitingRoom (); 
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		curPos = new Vector3 (roomCam.transform.position.x, roomCam.transform.position.y, roomCam.transform.position.z);
		enterRoom = true; 

		if (other.tag == "MessHall")
		{
			currentRoom = (int) rm.MessHall; 
			Debug.Log ("Entered mess hall.");
		}
		else if (other.tag == "EngineRoom")
		{
			currentRoom = (int) rm.EngineRoom;
			Debug.Log ("Entered engine room.");
		}
		else if (other.tag == "CrewQuarters")
		{
			currentRoom = (int) rm.CrewQuarters;;
			Debug.Log ("Entered crew quarters.");
		}
		else if (other.tag == "ObservationDeck")
		{
			currentRoom = (int) rm.ObservationDeck;
			Debug.Log ("Entered observation deck.");
		}
		else if (other.tag == "Bridge")
		{
			currentRoom = (int) rm.Bridge;
			Debug.Log ("Entered bridge.");
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		curPos = new Vector3 (roomCam.transform.position.x, roomCam.transform.position.y, roomCam.transform.position.z);

		if (other.tag == "MessHall" || other.tag == "EngineRoom" || other.tag == "CrewQuarters" || other.tag == "ObservationDeck" || other.tag == "Bridge")
		{
			exitRoom = true; 
			Debug.Log ("Moving through the ship.");
		}
	}

	void enteringRoom(){

		switch (currentRoom) {
		case (int) rm.MessHall:
			if (roomCam.orthographicSize <= camSizeOthers)
			{
				roomCam.orthographicSize = camSizeOthers; 
				enterRoom = false; 
			}
			else 
			{
				roomCam.orthographicSize -= sizeInc; 
			}
			roomCam.transform.position = Vector3.Slerp (curPos, new Vector3 (-0.575f, 0.175f, curPos.z), t);
			break;
		case (int) rm.EngineRoom:
			if (roomCam.orthographicSize <= camSizeOthers)
			{
				roomCam.orthographicSize = camSizeOthers; 
				enterRoom = false; 
			}
			else 
			{
				roomCam.orthographicSize -= sizeInc; 
			}
			roomCam.transform.position = Vector3.Slerp (curPos, new Vector3 (-0.465f, -2.175f, curPos.z), t);
			break;
		case (int) rm.CrewQuarters:
			if (roomCam.orthographicSize <= camSizeOthers)
			{
				roomCam.orthographicSize = camSizeOthers; 
				enterRoom = false; 
			}
			else 
			{
				roomCam.orthographicSize -= sizeInc; 
			} 
			roomCam.transform.position = Vector3.Slerp (curPos, new Vector3 (-5.0f, 0.175f, curPos.z), t);
			break;
		case (int) rm.ObservationDeck:
			if (roomCam.orthographicSize <= camSizeOthers)
			{
				roomCam.orthographicSize = camSizeOthers; 
				enterRoom = false; 
			}
			else 
			{
				roomCam.orthographicSize -= sizeInc; 
			} 
			roomCam.transform.position = Vector3.Slerp (curPos, new Vector3 (0.0f, 2.5f, curPos.z), t);
			Debug.Log ("Entered observation deck.");
			break;
		case (int) rm.Bridge:
			if (roomCam.orthographicSize <= camSizeBridge)
			{
				roomCam.orthographicSize = camSizeBridge; 
				enterRoom = false; 
			}
			else 
			{
				roomCam.orthographicSize -= sizeInc; 
			}
			roomCam.transform.position = Vector3.Slerp (curPos, new Vector3 (4.275f, 0.1f, curPos.z), t);
			Debug.Log ("Entered bridge.");
			break; 
		default:
			break; 
		}
	}

	void exitingRoom(){
		if (roomCam.orthographicSize >= camSizeExit)
		{
			roomCam.orthographicSize = camSizeExit; 
			exitRoom = false; 
		}
		else 
		{
			roomCam.orthographicSize += sizeInc; 
		}
		//roomCam.orthographicSize = Mathf.Lerp (roomCam.orthographicSize, camSizeExit, t); 
		roomCam.transform.position = Vector3.Slerp (curPos, new Vector3 (-0.575f, 0.175f, curPos.z), t);
	}
}
