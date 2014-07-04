using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour 
{
	// CAMERA
	public Camera mainCam;
	
	// TIME STUFF
	private float startTime;
	public float duration = 1.0f;
	
	// ZOOM IN/OUT STUFF
	public float camSizeBridge = 1.5f;
	public float camSizeOthers = 1.25f; 
	public float camSizeExit = 5.0f;
	
	private bool exitRoom;
	private bool enterRoom; 
	public Vector3 curPos; 
	private float t; 
	private enum rm { MessHall, EngineRoom, CrewQuarters, ObservationDeck, Bridge, OutsideRoom } ;
	private int currentRoom; 
	
	public float sizeInc;
	public float posInc; 
	
	// Use this for initialization
	void Start () 
	{
		
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		
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
	}
	
	void enteringRoom(){
		
		switch (currentRoom) {
		case (int) rm.MessHall:
			if (mainCam.orthographicSize <= camSizeOthers)
			{
				mainCam.orthographicSize = camSizeOthers; 
				enterRoom = false; 
			}
			else 
			{
				mainCam.orthographicSize -= sizeInc; 
			}
			mainCam.transform.position = Vector3.Slerp (curPos, new Vector3 (-0.575f, 0.175f, curPos.z), t);
			break;
		case (int) rm.EngineRoom:
			if (mainCam.orthographicSize <= camSizeOthers)
			{
				mainCam.orthographicSize = camSizeOthers; 
				enterRoom = false; 
			}
			else 
			{
				mainCam.orthographicSize -= sizeInc; 
			}
			mainCam.transform.position = Vector3.Slerp (curPos, new Vector3 (-0.465f, -2.175f, curPos.z), t);
			break;
		case (int) rm.CrewQuarters:
			if (mainCam.orthographicSize <= camSizeOthers)
			{
				mainCam.orthographicSize = camSizeOthers; 
				enterRoom = false; 
			}
			else 
			{
				mainCam.orthographicSize -= sizeInc; 
			} 
			mainCam.transform.position = Vector3.Slerp (curPos, new Vector3 (-5.0f, 0.175f, curPos.z), t);
			break;
		case (int) rm.ObservationDeck:
			if (mainCam.orthographicSize <= camSizeOthers)
			{
				mainCam.orthographicSize = camSizeOthers; 
				enterRoom = false; 
			}
			else 
			{
				mainCam.orthographicSize -= sizeInc; 
			} 
			mainCam.transform.position = Vector3.Slerp (curPos, new Vector3 (0.0f, 2.5f, curPos.z), t);
			Debug.Log ("Entered observation deck.");
			break;
		case (int) rm.Bridge:
			if (mainCam.orthographicSize <= camSizeBridge)
			{
				mainCam.orthographicSize = camSizeBridge; 
				enterRoom = false; 
			}
			else 
			{
				mainCam.orthographicSize -= sizeInc; 
			}
			mainCam.transform.position = Vector3.Slerp (curPos, new Vector3 (4.275f, 0.1f, curPos.z), t);
			Debug.Log ("Entered bridge.");
			break; 
		default:
			break; 
		}
	}
	
	void exitingRoom(){
		if (mainCam.orthographicSize >= camSizeExit)
		{
			mainCam.orthographicSize = camSizeExit; 
			exitRoom = false; 
		}
		else 
		{
			mainCam.orthographicSize += sizeInc; 
		}
		//mainCam.orthographicSize = Mathf.Lerp (mainCam.orthographicSize, camSizeExit, t); 
		mainCam.transform.position = Vector3.Slerp (curPos, new Vector3 (-0.575f, 0.175f, curPos.z), t);
	}
}