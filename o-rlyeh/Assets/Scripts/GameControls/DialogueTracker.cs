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
	public int lp; 		// pug love

	public int likeInc; 
	public int suspInc; 

	// TO UPDATE THE STATS WITHIN THE PROGRESS TRACKER
	public DialogueTracker ptrack; 

	// DIALOGUE OPTIONS/DATA STRUCTURES
	public struct dialogueSegment	// struct: each block of dialogue
	{
		public string paragraph; 	// what the cpt/pug will say
		public int numOpts;			// 0 if no options, 3 if there are options
		public int numResp; 
		public string[] options; 	// the player's possible responses to the paragraph
		public string[] responses; // NPC response to the option picked

		public dialogueSegment (string par, int m, string opt0, string opt1, string opt2, int n, string resp0, string resp1, string resp2)
		{
			paragraph = par; 
			numOpts = m;
			numResp = n;
			if (m > 0)
			{
				options = new string[m];
				options[0] = opt0;
				options[1] = opt1;
				options[2] = opt2; 
			}
			else 
			{
				options = null; 
			}
			if (n > 0){
				responses = new string[3]; 
				responses[0] = resp0;
				responses[1] = resp1;
				responses[2] = resp2;
			}
			else 
			{
				responses = null; 
			}
		}
	}

	// first index: the day, second index: the conversation (depending on liking/suspicion level)
	private dialogueSegment[][] pug;	// pug conversations
	private dialogueSegment[][] cpt; 	// captain conversations
	private dialogueSegment[]	quest;	// one quest per day therefore 1D array

	// STRING CHECKING TO UPDATE STATS
	private string addSusp 	= "(suspUp)";
	private string addLike 	= "(likeUp)";
	private string addLP 	= "(LPUp)"; 

	// FOR DIALOGUE/GAME FLOW
	public int day;				// 0 to 4 = 5 days total 
	public int conversation;	// where are we in the dialogue (second index of matrix)

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

	public string passDialogueToGUI(string s)
	{
		return null; 
	}
	
	/* --------------------------------------------------------------------------------------------------------------------------
	 * NO ARGS. NO RETURN. 
	 * when enter room corresponding to the daily quest, display the quest options
	 * -------------------------------------------------------------------------------------------------------------------------- */
	private void generateQuestOptions(){
		if (receivedCptQuest) 
		{
			// show the option to do cpt quest
		}
		if (receivedPugQuest)
		{
			// show the option to do pug quest
		}
	}

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
		ptrack.ap 	= ap;
		ptrack.like = like;
		ptrack.susp = susp; 
		ptrack.lp 	= lp; 
	}

	/* --------------------------------------------------------------------------------------------------------------------------
	 * NO ARGS. NO RETURN. 
	 * IN START, save all the dialogues
	 * -------------------------------------------------------------------------------------------------------------------------- */
	private void makeConversations()
	{
		// DAY 1 - CAPTAIN

		//string par, int n, string opt0, string opt1, string opt2, string resp0, string resp1, string resp2
		cpt [0] [0] = new dialogueSegment ("Hey there.", 
		                                  	3,	"OFFICIAL ANSWER" + addLike, 
		                                   		"CASUAL ANSWER" + addSusp, 
		                                   		"...",
		                                  	0, null, null, null); 

		cpt [0] [1] = new dialogueSegment ("You seem a little different today. Is everything ok?", 
		                                  	3,	"Everything's fine.", 
		                                   		"FLATTER HIM" + addLike, 
		                                   		"...",
		                                  	0, null, null, null);

		cpt [0] [2] = new dialogueSegment ("Any changes in the readings?", 
		                                  	3,	"That's what I wanted to talk to you about. They're going crazy! We need to leave as soon as possible!" + addSusp, 
		                                 		"Well, the readings are showing some concerning results, but nothing certain yet.",
		                                   		"All is well, Captain.", 
		                                  	3,	"Uh, are you sure it's not just a misreading?",
				                                "Is that so? Hmm, that is a concern... Please let me know if the readings change.",
		                                   		"Good to hear, keep up the good work.");

		cpt [0] [3] = new dialogueSegment ("Well, now that you're here I do actually have a task you could help me with. I know you're a researcher, not a technician, but I'd need you to go down to maintenance and twist a double of knobs. I want to avoid leaving my post now that we're getting so close to the anomaly, so I'd really appreciate your help. Will you do it?",
		                                  	3, "Of course, sir." + addLike, 
		                                  		"I have better things to do, but very well.",
		                                  		"..." + addSusp,
		                                  	0, null, null, null); 

		cpt [0] [4] = new dialogueSegment ("You really do seem different today... Maybe I'm just tired.",
		                                   0, null, null, null,
		                                   0, null, null, null);

		// DAY 1 - PUG
		pug [0] [0] = new dialogueSegment ("You return, minion.",
		                                   	3,	"Yes master, I wanted to bask in your allmighty glory to start the day." + addLP,
		                                   		"Yes, glorious master.",
		                                   		"...",
		                                  	0, null, null, null); 
		pug [0] [1] = new dialogueSegment (null, 
		                                   	2,	"Bask in Ammutsepug's glory" + addSusp + addLP,
		                                  		"Ask Ammutsepug for a task.",
		                                   		null,
		                                   	0, null, null, null);

		// THIS ONLY TRIGGERS IF THE PLAYER SELECTED OPTION 2 (ASK FOR A TASK)
		pug [0] [2] = new dialogueSegment ("Ah, you come to me for advice. A wise decision. This machine, this is what is bringing you towards me, is it not? Find a way to sabotage the machine so that it can not move towards me anymore.",
		                                   3,	"Yes, Great Slobbery One, Devourer of Stars (and Treats). I will gladly do your bidding, you beautiful being." + addLP,
		                                   		"Very well, I will do your bidding.",
		                                   		"...", 
		                                   0, null, null, null);

		// DAY 1 - QUEST
		quest [0] = new dialogueSegment ("What do you want to do?", 
		                                 3, "Twist knobs",
		                                 	"Sabotage",
		                                 	"Do nothing", 
		                                 0, null, null, null); 

		// DAY 2 - CAPTAIN

		// IF PLAYER DID TASK, SHOW THIS. OTHERWISE, SKIP cpt[1][0]
		cpt [1] [0] = new dialogueSegment ("Ah, there you are! Good job yesterday, things are working as they should.", 
		                                	3,	"OFFICIAL RESPONSE" + addLike,
		                                 		"SMUG RESPONSE",
		                                 		"DEFER COMPLIMENT" + addLike, 
		                                 	0, null, null, null); 

		cpt [1] [1] = new dialogueSegment ("So, any changes in the readings today?", 
		                                   3,	"That's what I wanted to talk to you about. They're going crazy! We need to leave as soon as possible!" + addSusp, 
		                                   		"Well, the readings are showing some concerning results, but nothing certain yet.",
		                                   		"All is well, Captain.", 
		                                   3,	"Uh, are you sure it's not just a misreading?",
		                                   		"Is that so? Hmm, that is a concern... Please let me know if the readings change.",
		                                   		"Good to hear, keep up the good work.");

		cpt [1] [1] = new dialogueSegment ("(YOU NOTICE THAT THE CAPTAIN LOOKS TIRED)", 
		                                   3,	"Sir, you look quite tired. Would you like me to get you a caffeinated beverage pouch?" + addLike, 
		                                  		"Sir, you're looking a bit tired.",
		                                   		"...", 
		                                   3,	"That would be great! Thank you!",
		                                   		"Yes, I'm feeling quite tired indeed... Get me a pouch of coffee, will you?",
		                                   		"Get me a pouch of coffee, will you?");

		cpt [1] [2] = new dialogueSegment ("Oh and I like it with milk, no sugar.", 
		                                	0, null, null, null,
		                                	0, null, null, null);

		// DAY 2 - PUG 

		// IF DID CAPTAIN'S QUEST OR NOTHING
		pug [1] [0] = new dialogueSegment ("You defy me, minion? You dare?! The ship is still moving towards me!",
		                                   3,	"MAKE EXCUSES",
		                                   		"GROVEL AND APOLOGIZE",
		                                   		"...",
		                                   1, 	"No matter. There is still time.", null, null); 

		// IF DID PUG QUEST
		pug [1] [1] = new dialogueSegment ("Well done, my minion. The ship has slowed down!		",
		                                   3,	"GRACIOUSLY ACCEPT" + addLP,
		                                   		"RELUCTANTLY ACCEPT",
		                                   		"Graciously accept and praise the pug lord." + addLP,
		                                   0, null, null, null); 

		pug [1] [2] = new dialogueSegment ("Ah, you come to me for advice. A wise decision. This machine, this is what is bringing you towards me, is it not? Find a way to sabotage the machine so that it can not move towards me anymore.",
		                                   3,	"Yes, Great Slobbery One, Devourer of Stars (and Treats). I will gladly do your bidding, you beautiful being.",
		                                   		"Very well, I will do your bidding.",
		                                   		"...",
		                                   0, null, null, null);

		// DAY 2 - QUEST
		quest [1] = new dialogueSegment ("What do you want to do?", 
		                                 3, "Get coffee with milk" + addLike,
		                                 	"Get coffee with sugar",
		                                 	"Poison coffee" + addLP, 
		                                 0, null, null, null);
	}
}
