using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "MessHall")
			Debug.Log ("Entered mess hall.");
		else if (other.tag == "EngineRoom")
			Debug.Log ("Entered engine room.");
		else if (other.tag == "CrewQuarters")
			Debug.Log ("Entered crew quarters.");
		else if (other.tag == "ObservationDeck")
			Debug.Log ("Entered observation deck.");
		else if (other.tag == "Bridge")
			Debug.Log ("Entered bridge.");

	}
}
