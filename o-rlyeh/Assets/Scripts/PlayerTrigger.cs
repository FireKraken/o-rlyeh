using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour 
{
	public float duration = 1.0f;

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

	void OnTriggerEnter2D (Collider2D other)
	{
		Vector3 curPos = new Vector3 (roomCam.transform.position.x, roomCam.transform.position.y, roomCam.transform.position.z);

		float t = (Time.time - startTime) / duration;

		if (other.tag == "MessHall")
		{
			roomCam.orthographicSize = 1.25f;
			roomCam.transform.position = Vector3.Lerp (curPos, new Vector3 (-0.575f, 0.175f, curPos.z), t);
			Debug.Log ("Entered mess hall.");
		}
		else if (other.tag == "EngineRoom")
		{
			roomCam.orthographicSize = 1.25f;
			roomCam.transform.position = Vector3.Lerp (curPos, new Vector3 (-0.465f, -2.175f, curPos.z), t);
			Debug.Log ("Entered engine room.");
		}
		else if (other.tag == "CrewQuarters")
		{
			roomCam.orthographicSize = 1.25f;
			roomCam.transform.position = Vector3.Lerp (curPos, new Vector3 (-5.0f, 0.175f, curPos.z), t);
			Debug.Log ("Entered crew quarters.");
		}
		else if (other.tag == "ObservationDeck")
		{
			roomCam.orthographicSize = 1.25f;
			roomCam.transform.position = Vector3.Lerp (curPos, new Vector3 (0.0f, 2.5f, curPos.z), t);
			Debug.Log ("Entered observation deck.");
		}
		else if (other.tag == "Bridge")
		{
			roomCam.orthographicSize = 1.5f;
			roomCam.transform.position = Vector3.Lerp (curPos, new Vector3 (4.275f, 0.1f, curPos.z), t);
			Debug.Log ("Entered bridge.");
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		Vector3 curPos = new Vector3 (roomCam.transform.position.x, roomCam.transform.position.y, roomCam.transform.position.z);
		
		float t = (Time.time - startTime) / duration;

		if (other.tag == "MessHall" || other.tag == "EngineRoom" || other.tag == "CrewQuarters" || other.tag == "ObservationDeck" || other.tag == "Bridge")
		{
			roomCam.orthographicSize = 5.0f;
			roomCam.transform.position = Vector3.Lerp (curPos, new Vector3 (-0.575f, 0.175f, curPos.z), t);
			Debug.Log ("Moving through the ship.");
		}
	}
}
