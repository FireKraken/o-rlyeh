using UnityEngine;
using System.Collections;

public class DialogueScript : MonoBehaviour 
{
	private string npcDialogue = "Schmaltzy dialogue goes here...";

	private bool showLog = false;
	private Rect logRect;

	// Use this for initialization
	void Start () 
	{
		logRect = new Rect (20, 20, 350, Screen.height * 2 / 3);	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnGUI ()
	{
		GUILayout.BeginArea (new Rect (Screen.width * 0.1f, Screen.height * 0.1f, Screen.width * 0.8f, Screen.height * 0.8f));
		GUILayout.BeginVertical ();

		if (showLog)
			logRect = GUI.Window (0, logRect, LogWindowFunction, "Log");

		GUILayout.Space (Screen.height * 2 / 3);

		GUILayout.BeginHorizontal ();

		GUILayout.BeginVertical ();
		GUILayout.FlexibleSpace ();
		GUILayout.EndVertical ();

		GUILayout.Box (npcDialogue);

		GUILayout.BeginVertical ();
		GUILayout.FlexibleSpace ();
		GUILayout.EndVertical ();

		GUILayout.EndHorizontal ();

		GUILayout.EndVertical ();
		GUILayout.EndArea ();
	}

	void LogWindowFunction (int windowID)
	{
		GUI.DragWindow ();
	}
}
