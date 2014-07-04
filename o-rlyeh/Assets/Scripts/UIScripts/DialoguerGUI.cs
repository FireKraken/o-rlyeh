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

	// Use this for initialization
	void Start () 
	{
		// Listeners for the following events
		Dialoguer.events.onStarted += onStarted;
		Dialoguer.events.onEnded += onEnded;
		Dialoguer.events.onTextPhase += onTextPhase;
	}
	
	void OnGUI ()
	{
		if (!_showing)
			return;

		GUILayout.Box (_text);

		if (_choices == null)
		{
			if (GUILayout.Button ("Next"))
			{
				Dialoguer.ContinueDialogue ();
			}
		}
		else
		{
			for (int i = 0; i < _choices.Length; i++)
			{
				if (GUILayout.Button ((i + 1) + ".\t" + _choices[i]))
				{
					Dialoguer.ContinueDialogue (i);
				}
			}
		}
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
