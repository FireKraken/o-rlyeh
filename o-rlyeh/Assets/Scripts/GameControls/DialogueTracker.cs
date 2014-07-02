using UnityEngine;
using System.Collections;

// ATTACH TO GAME CONTROLLER
// controls the dialogue (which conversations are triggered)

public class DialogueTracker : MonoBehaviour {

	// PLAYER STATS
	public int ap;		// action points - max 5 (regenerate if sleep, -- if talk or action)
	public int like;	// liking level - 0 to 10 
	public int susp; 	// suspicion level - 0 to 4

	// TO UPDATE THE STATS WITHIN THE PROGRESS TRACKER
	public DialogueTracker ptrack; 

	// DIALOGUE OPTIONS/DATA STRUCTURES
	private struct dialogueSegment	// struct: each block of dialogue
	{
		private string paragraph; 	// what the cpt/pug will say
		private int size;			// 0 if no options, 3 if there are options
		private string[] options; 	// the player's possible responses to the paragraph
		private string[] responses; // NPC response to the option picked
	}

	// first index: the day, second index: the conversation (depending on liking/suspicion level)
	private dialogueSegment[][] pug;	// pug conversations
	private dialogueSegment[][] cpt; 	// captain conversations

	void Start () 
	{
		
	}
	

	void Update () 
	{

	}

	/* --------------------------------------------------------------------------------------------------------------------------
	 * NO ARGS. NO RETURN. 
	 * updates the player stat values (ap, like, susp) in the dialogue tracker to match the ones in progress tracker
	 * -------------------------------------------------------------------------------------------------------------------------- */
	
	private void updatePStats()
	{
		ptrack.ap = ap;
		ptrack.like = like;
		ptrack.susp = susp; 
	}
}
