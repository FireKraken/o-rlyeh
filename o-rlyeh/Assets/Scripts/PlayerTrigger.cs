using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour 
{
	public float duration = 0.05f;

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
		Vector3 curPos = new Vector3 (0, 0, 0);
		curPos.x = mainCam.transform.position.x;
		curPos.y = mainCam.transform.position.y;
		curPos.z = mainCam.transform.position.z;

		if (other.tag == "MessHall")
		{
			mainCam.orthographicSize = 1.25f;
			mainCam.transform.position = new Vector3 (Mathf.SmoothStep (curPos.x, -0.575f, duration), Mathf.SmoothStep (curPos.y, 0.175f, duration), curPos.z);
			Debug.Log ("Entered mess hall.");
		}
		else if (other.tag == "EngineRoom")
		{
			mainCam.orthographicSize = 1.25f;
			mainCam.transform.position = new Vector3 (Mathf.SmoothStep (curPos.x, -0.465f, duration), Mathf.SmoothStep (curPos.y, -2.175f, duration), curPos.z);
			Debug.Log ("Entered engine room.");
		}
		else if (other.tag == "CrewQuarters")
		{
			mainCam.orthographicSize = 1.25f;
			mainCam.transform.position = new Vector3 (Mathf.SmoothStep (curPos.x, -5.0f, duration), Mathf.SmoothStep (curPos.y, 0.175f, duration), curPos.z);
			Debug.Log ("Entered crew quarters.");
		}
		else if (other.tag == "ObservationDeck")
		{
			mainCam.orthographicSize = 1.25f;
			mainCam.transform.position = new Vector3 (Mathf.SmoothStep (curPos.x, 0.0f, duration), Mathf.SmoothStep (curPos.y, 2.5f, duration), curPos.z);
			Debug.Log ("Entered observation deck.");
		}
		else if (other.tag == "Bridge")
		{
			mainCam.orthographicSize = 1.5f;
			mainCam.transform.position = new Vector3 (Mathf.SmoothStep (curPos.x, 4.275f, duration), Mathf.SmoothStep (curPos.y, 0.1f, duration), curPos.z);
			Debug.Log ("Entered bridge.");
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		Vector3 curPos = new Vector3 (0, 0, 0);
		curPos.x = mainCam.transform.position.x;
		curPos.y = mainCam.transform.position.y;
		curPos.z = mainCam.transform.position.z;

		if (other.tag == "MessHall" || other.tag == "EngineRoom" || other.tag == "CrewQuarters" || other.tag == "ObservationDeck" || other.tag == "Bridge")
		{
			mainCam.orthographicSize = 5.0f;
			mainCam.transform.position = new Vector3 (Mathf.SmoothStep (curPos.x, -0.575f, duration), Mathf.SmoothStep (curPos.y, 0.175f, duration), curPos.z);
			Debug.Log ("Moving through the ship.");
		}
	}
}
