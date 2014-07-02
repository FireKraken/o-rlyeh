using UnityEngine;
using System.Collections;

// ATTACH TO GAME CONTROLLER
// controls the dialogue (which conversations are triggered)
// updates player stats depending on dialogue responses

public class DialogueTracker : MonoBehaviour {

	// PLAYER STATS
	public int ap;		// action points - max 5 (regenerate if sleep, -- if talk or action)
	public int like;	// liking level - 0 to 10 
	public int susp; 	// suspicion level - 0 to 4

	public int likeInc; 
	public int suspInc; 

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

	// STRING CHECKING TO UPDATE STATS
	private string addSusp = "add suspicion";
	private string addLike = "add liking"; 

	// FOR DIALOGUE/GAME FLOW
	public int day;			// 0 to 4 = 5 days total 

	public bool receivedCptQuest;	// true if speak to the CPT and you get cpt quest
	public bool receivedPugQuest;	// true if speak to the PUG and you get pug quest

	public bool completedCptQuest;	// true if you do what the CPT asks
	public bool completedPugQuest;	// true if you do what the PUG asks

	

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


	
	/* --------------------------------------------------------------------------------------------------------------------------
	 * NO ARGS: string, the option the player picked to answer the NPC
	 * (1) if option contains the string addSusp, then increment suspicion
	 * (2) if option contains the string addLike, then increment liking 
	 * (3) update the stats in progress tracker
	 * -------------------------------------------------------------------------------------------------------------------------- */
	private void updateDStats(string option)
	{
		// (1) add suspicion 
		if (option.Contains (addSusp)) 
		{
			susp += suspInc;
		}

		// (2) add liking
		else if (option.Contains (addLike))
		{
			like += likeInc; 
		}

		// (3) update in progress tracker
		updatePStats (); 
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
