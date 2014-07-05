using UnityEngine;
using System.Collections;

public class InitializeDialogue : MonoBehaviour 
{
	void Awake ()
	{
		Dialoguer.Initialize ();
	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnGUI ()
	{
		if (GUILayout.Button ("Begin") || GameObject.Find ("FemalePlayer").GetComponent<PlayerTrigger> ().pauseAction)
		{
			Dialoguer.StartDialogue (0, dialoguerCallback);
			this.enabled = false;
		}
	}

	private void dialoguerCallback ()
	{
		this.enabled = true;
	}
}
