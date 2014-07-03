using UnityEngine;
using System.Collections;

public class DialoguerGUI : MonoBehaviour 
{
	// Used to disable and enable GUI for dialogue
	private bool _showing;

	// Stores text data from Dialoguer
	private string _text;

	// Stores choice data
	private string[] _choices;

	// Use this for initialization
	void Start () 
	{
		// Listeners for Dialoguer events
		Dialoguer.events.onStarted += onStarted;
		Dialoguer.events.onEnded += onEnded;
		Dialoguer.events.onTextPhase += onTextPhase;
	}

	void OnGUI ()
	{
		if (!_showing)
			return;

		// GUI.Box (new Rect (10, 10, 200, 150), _text);
		GUILayout.Box (_text);

		if (_choices == null)
		{
			if (GUI.Button (new Rect (10, 220, 200, 30), "Next"))
			{
				Dialoguer.ContinueDialogue ();
			}
		}
		else
		{
			for (int i = 0; i < _choices.Length; i++)
			{
				if (GUI.Button (new Rect (10, 200 + 40 * i, 200, 30), "Choice " + (i + 1)))
				{
					Dialoguer.ContinueDialogue (i);
				}
			}
		}
	}

	// Called when dialogue starts
	private void onStarted ()
	{
		_showing = true;
	}

	// Called when dialogue ends
	private void onEnded ()
	{
		_showing = false;
	}

	// Called when Dialoguer is serving new text data
	private void onTextPhase (DialoguerTextData data)
	{
		_text = data.text;
		_choices = data.choices;
	}
}
