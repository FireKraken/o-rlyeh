using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour 
{
	public float duration = 1.0f;

	public Camera mainCam;

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
		Vector3 curPos = new Vector3 (mainCam.transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z);

		float t = (Time.time - startTime) / duration;

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
	}

	void OnTriggerExit2D (Collider2D other)
	{
		Vector3 curPos = new Vector3 (mainCam.transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z);
		
		float t = (Time.time - startTime) / duration;

		if (other.tag == "MessHall" || other.tag == "EngineRoom" || other.tag == "CrewQuarters" || other.tag == "ObservationDeck" || other.tag == "Bridge")
		{
			mainCam.orthographicSize = 5.0f;
			mainCam.transform.position = Vector3.Slerp (curPos, new Vector3 (-0.575f, 0.175f, curPos.z), t);
			Debug.Log ("Moving through the ship.");
		}
	}
}
