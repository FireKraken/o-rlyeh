using UnityEngine;
using System.Collections;

public class DialoguerGUI : MonoBehaviour 
{
	// Used to show and hide GUI on dialogue start and end
	private bool _showing;

	// Used to store text data
	private string _text;

	// Used to store text from choice data
	private string[] _choices;

	public GUIStyle _custom;

	// Use this for initialization
	void Start () 
	{
		// Listeners for the following events
		Dialoguer.events.onStarted += onStarted;
		Dialoguer.events.onEnded += onEnded;
		Dialoguer.events.onTextPhase += onTextPhase;

		_custom.fontSize = Screen.width / 90;
	}
	
	void OnGUI ()
	{
		if (!_showing)
			return;

		GUILayout.BeginArea (new Rect (Screen.width * 0.05f, Screen.height * 0.05f, Screen.width * 0.85f, Screen.height * 0.85f));

		GUILayout.Space (Screen.height * 7 / 12);

		GUILayout.BeginHorizontal ();

		GUILayout.BeginVertical ();

		GUILayout.Box (_text, _custom);

		GUILayout.Space (Screen.height * 1 / 12);

		if (_choices == null)
		{
			if (GUILayout.Button (">>", _custom))
			{
				Dialoguer.ContinueDialogue ();
			}
		}
		else
		{
			for (int i = 0; i < _choices.Length; i++)
			{
				if (GUILayout.Button ((i + 1) + ".\t" + _choices[i], _custom))
				{
					Dialoguer.ContinueDialogue (i);
				}
			}
		}

		GUILayout.EndVertical ();

		GUILayout.FlexibleSpace ();

		GUILayout.EndHorizontal ();

		GUILayout.EndArea ();
	}

	// Called when dialogue starts
	private void onStarted ()
	{
		// Show GUI
		_showing = true;
	}

	// Called when dialogue ends
	private void onEnded ()
	{
		// Hide GUI
		_showing = false;
	}

	// Called when dialoguer is serving new text data
	private void onTextPhase (DialoguerTextData data)
	{
		// Store text data from current phase
		_text = data.text;
		_choices = data.choices;
	}
}
