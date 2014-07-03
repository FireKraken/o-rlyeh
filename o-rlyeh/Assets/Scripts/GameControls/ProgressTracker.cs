using UnityEngine;
using System.Collections;

// ATTACH TO GAME CONTROLLER
// updates the player stats and controls the general flow of the game
// most variables are public for debugging reasons

public class ProgressTracker : MonoBehaviour {

	// PLAYER STATS - these are updated in THIS (ProgressTracker) script 
	public int ap;		// action points - max 5 (regenerate if sleep, -- if talk or action)
	public int like;	// liking level - 0 to 10 
	public int susp; 	// suspicion level - 0 to 4
	public int lp;		// pug love

	private int maxLike = 10; 
	private int maxSusp = 4; 

	// TO UPDATE THE STATS WITHIN THE DIALOGUE TRACKER
	public DialogueTracker dtrack; 

	// GAME FLOW STUFF
	public int day;			// 0 to 4 = 5 days total 
	private int numDays = 2; 	// total number of days: currently 2

	public bool receivedCptQuest;	// true if speak to the cpt and you get cpt quest
	public bool receivedPugQuest;	// true if speak to the pug and you get pug quest
	public bool completedCptQuest;	// true if you do what the CPT asks
	public bool completedPugQuest;	// true if you do what the PUG asks

	void Start () 
	{
		
	}

	void Update () 
	{

		if (day > numDays) 
		{
			generateEnding (); 
		}
	}


	/* --------------------------------------------------------------------------------------------------------------------------
	 * NO ARGS. NO RETURN. 
	 * if surpassed total number of days (i.e. exiting wormhole), generate one of currently 4 endings
	 * -------------------------------------------------------------------------------------------------------------------------- */



	/* --------------------------------------------------------------------------------------------------------------------------
	 * NO ARGS. NO RETURN. 
	 * if surpassed total number of days (i.e. exiting wormhole), generate one of currently 4 endings
	 * -------------------------------------------------------------------------------------------------------------------------- */
	private void generateEnding(){
		// ending 1 - full liking = CPT DIES FOR YOU
		// ending 2 - 2/3 liking & Max 1/3  suspicion = CPT REVEALS KEY CODE
		// ending 3 - less than 2/3 &  More than 1/3 suspicion = SHIP FLIES INTO PUG
		// ending 4 - full suspicion reached = ELDER PUG REVEALED
		bool end1 = (like >= maxLike);
		bool end2 = (like >= Mathf.FloorToInt ((float) maxLike * (2.0f / 3.0f)) && susp <= Mathf.FloorToInt ((float) maxSusp * (1.0f / 3.0f)));
		bool end3 = (like < Mathf.FloorToInt ((float) maxLike * (2.0f / 3.0f)) && susp > Mathf.FloorToInt ((float) maxSusp * (1.0f / 3.0f)));
		bool end4 = (susp >= maxSusp);

		if (end1) 
		{
			Application.LoadLevel ("CptDiesForYou");
		}
		if (end2) 
		{
			Application.LoadLevel ("CptRevealsKey");
		}
		if (end4) 
		{
			Application.LoadLevel ("PugRevealed");
		}
		// DEFAULT ENDING: mission completes = ship crashes into pug
		if (end3)
		{
			Application.LoadLevel ("PugDies");
		}
	}

	/* --------------------------------------------------------------------------------------------------------------------------
	 * NO ARGS. NO RETURN. 
	 * updates the player stat values (ap, like, susp) in the dialogue tracker to match the ones in progress tracker
	 * -------------------------------------------------------------------------------------------------------------------------- */

	private void updateDStats()
	{
		dtrack.ap = ap;
		dtrack.like = like;
		dtrack.susp = susp; 
		dtrack.lp = lp; 
	}
}
