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
		
		public dialogueSegment(string par, int m, string[] opts, int n, string[] resps)
		{
			paragraph = par; 
			numOpts = m;
			numResp = n;
			
			if (m > 0)
			{
				options = new string[m];
				for (int i = 0; i < m; i++)
				{
					options[i] = opts[i]; 
				}
			}
			else 
			{
				options = null; 
			}
			
			if (n > 0)
			{
				responses = new string[n];
				for (int i = 0; i < n; i++)
				{
					responses[i] = resps[i]; 
				}
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
		makeConversations ();
		
		
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
		cpt = new dialogueSegment [2][]; 
		pug = new dialogueSegment [2][];
		quest = new dialogueSegment [2];
		string[] opts;
		string[] resps; 
		
		//struct constructor: string par, int m, string[] opts, int n, string[] resps
		
		// ----- DAY 1 - CAPTAIN -----
		
		// CPT (0-0)
		opts = new string[3] {	"Sir." + addLike, 
			"Hi there." + addSusp, 
			"..."}; 
		
		cpt [0] [0] = new dialogueSegment ("Hello, Science Officer.", 
		                                   3, opts,
		                                   0, null); 
		
		// CPT (0-1)
		opts = new string[3] {	"Everything's fine.", 
			"Working with a great Captain like you, things couldn't be better." + addLike, 
			"..."}; 
		
		cpt [0] [1] = new dialogueSegment ("You seem a little different today. Is everything ok?", 
		                                   3, opts,
		                                   0, null);
		
		// CPT (0-2)
		opts = new string[3] {	"I wanted to discuss just that; the readings are off all known charts! We need to leave as soon as possible!" + addSusp, 
			"Sir, the readings are showing some abnormal results, but nothing conclusive yet." + addLike, 
			"All is well, Captain."}; 
		
		resps = new string[3] {	"Uh, that's very sudden. Surely it's just a misreading. Please check your calculations before you decide that we need to abort our mission.",
			"Is that so? Hmm, that is a concern... Please let me know if the readings change.",
			"Good to hear, keep up the good work."};
		
		cpt [0] [2] = new dialogueSegment ("Hm, very well. Any changes in the readings?", 
		                                   3, opts,  
		                                   3, resps);
		
		// CPT (0-3)
		opts = new string[3] { 	"Anything for you, sir." + addLike,
			"I have better things to do, but very well.",
			"..." + addSusp }; 
		
		cpt [0] [3] = new dialogueSegment ("Well, now that you're here I do actually have a task you could help me with. I know you're a scientist, not a technician, but I'd need you to go down to maintenance and twist a double of knobs. I want to avoid leaving my post now that we're getting so close to the anomaly. Will you do it?",
		                                   3, opts,
		                                   0, null); 
		
		// CPT (0-4)
		cpt [0] [4] = new dialogueSegment ("You know, you really do seem different today... Maybe I'm just tired.",
		                                   0, null,
		                                   0, null);
		
		// ----- DAY 1 - PUG -----
		
		// PUG (0-0)
		opts = new string[3] { 	"Yes my beautiful master, I wanted to stare into your starry eyes." + addLP,
			"Yes master, I wanted to bask in your almighty glory.",
			"Yes, master."}; 
		
		pug [0] [0] = new dialogueSegment ("You return, minion.",
		                                   3, opts,
		                                   0, null); 
		
		// PUG (0-1)
		opts = new string[2] { 	"Bask in Ammutsepug's glory" + addSusp + addLP,
			"Ask Ammutsepug for a task."}; 
		
		pug [0] [1] = new dialogueSegment (null, 
		                                   2, opts,
		                                   0, null);
		
		// PUG (0-2) * THIS ONLY TRIGGERS IF THE PLAYER SELECTED OPTION 2 (ASK FOR A TASK)
		opts = new string[3] { 	"Yes, Great Slobbery One, Devourer of Stars (and Treats). I will gladly do your bidding, you glorious being." + addLP,
			"Very well, I will do your bidding.",
			"Yes, master."};  
		
		pug [0] [2] = new dialogueSegment ("Ah, you come to me for advice. A wise decision. This machine, this is what is bringing you towards me, yes? Find a way to sabotage the machine, minion",
		                                   3, opts,		 
		                                   0, null);
		
		// ----- DAY 1 - QUEST -----
		opts = new string[3] { 	"Twist knobs",
			"Sabotage",
			"Do nothing"};  
		
		quest [0] = new dialogueSegment ("What do you want to do?", 
		                                 3, opts, 
		                                 0, null); 
		
		// ----- DAY 2 - CAPTAIN -----
		
		// CPT (1-0) * IF PLAYER DID TASK, SHOW THIS. OTHERWISE, SKIP cpt[1][0]
		opts = new string[3] { 	"Sir, thank you sir." + addLike,
			"Of course I did, it was child's play.",
			"It's all thanks to your great leadership, my Captain." + addLike};
		
		cpt [1] [0] = new dialogueSegment ("Ah, there you are! Good job yesterday, things are working as they should.", 
		                                   3, opts, 
		                                   0, null); 
		
		// CPT (1-1) * IF PLAYER SABOTAGED, SHOW THIS. OTHERWISE, SKIP cpt[1][0]
		opts = new string[3] { 	"It is?! I didn't notice at all! I did exactly what you asked me to!!!." + addSusp,
			"Yes, I noticed that too... Maybe I twisted the wrong knob?",
			"..." + addSusp};
		
		cpt [1] [1] = new dialogueSegment ("There you are. Maybe I shouldn't have sent you to do the task yesterday after all, the ship's acting weird..", 
		                                   3, opts, 
		                                   0, null);
		
		// CPT (1-2) * IF PLAYER DIDN'T DO TASK, SHOW THIS. OTHERWISE, SKIP cpt[1][0]
		opts = new string[3] { 	"Yes sir, I'm sorry sir. It will not happen again." + addLike,
			"I'm sorry, something came up and... Something came up." + addSusp,
			"..." + addSusp};
		
		cpt [1] [2] = new dialogueSegment ("You! If you weren't going to do the task you should have told me so! I had to go and do it myself!", 
		                                   3, opts, 
		                                   0, null);
		
		// CPT (1-3) * THIS HAPPENS REGARDLESS
		opts = new string[3] {	"I wanted to discuss just that; the readings are off all known charts! We need to leave as soon as possible!" + addSusp, 
			"Sir, the readings are showing some abnormal results, but nothing conclusive yet." + addLike, 
			"All is well, Captain."}; 
		
		resps = new string[3] {	"Uh, that's very sudden. Surely it's just a misreading. Please check your calculations before you decide that we need to abort our mission.",
			"Is that so? Hmm, that is a concern... Please let me know if the readings change.",
			"Good to hear, keep up the good work."};
		
		cpt [1] [3] = new dialogueSegment ("So, any changes in the readings today?", 
		                                   3, opts, 
		                                   3, resps);
		
		// CPT (1-4)
		opts = new string[3] {	"Sir, you look quite fatigued. Would you like me to get you a caffeinated beverage pouch?" + addLike, 
			"Sir, you look quite exhausted.", 
			"..."}; 
		
		resps = new string[3] {	"That would be great! Thank you!",
			"Yes, I'm definitely feeling tired... Get me a pouch of coffee, will you?",
			"Get me a pouch of coffee, will you?"};
		
		cpt [1] [4] = new dialogueSegment ("(YOU NOTICE THAT THE CAPTAIN LOOKS TIRED)", 
		                                   3, opts, 
		                                   3, resps);
		// CPT (1-5)
		cpt [1] [5] = new dialogueSegment ("Oh and I like my coffee with milk, no sugar. Thanks!", 
		                                   0, null,
		                                   0, null);
		
		// ----- DAY 2 - PUG -----
		
		// PUG (1-0) * IF DID CAPTAIN'S QUEST OR NOTHING
		opts = new string[3] {	"I was only doing what I though would be best! I was concerned the Captain would get suspicious!",
			"O Great Slobbery One, with your starry fur and black abyss of a muzzle! Have pity on this lowly mortal being, give me another chance!",
			"..."}; 
		
		resps = new string[1] { "No matter. There is still time." };
		
		pug [1] [0] = new dialogueSegment ("You defy me, minion? You dare?! The ship is still moving towards me!",
		                                   3, opts,  		
		                                   1, resps); 
		
		// PUG (1-1) * IF DID PUG QUEST
		opts = new string[3] {	"Of course, O Great Slobbery One!" + addLP,
			"I simply did what you asked of me.",
			"Anything for you, beautiful being. Anything and everything for your starburst eyes and nebulous form." + addLP}; 
		
		pug [1] [1] = new dialogueSegment ("Well done, my minion. The ship has slowed down!",
		                                   3, opts,
		                                   0, null); 
		// PUG (1-2)
		opts = new string[3] { 	"Yes, Great Slobbery One, Devourer of Stars (and Treats). I will gladly do what you ask, you beautiful being." + addLP,
			"Very well, I will do your bidding.",
			"..."};  
		
		pug [1] [2] = new dialogueSegment ("It's time to get rid of the troublesome captain. I tried to connect with him like I do with you, but he resisted somehow. It is probable his mind is too simple to accept my greatness. Maybe you could find a substance in the mess hall that you could poison?",
		                                   3, opts,
		                                   0, null);
		
		// ----- DAY 2 - QUEST -----
		opts = new string[3] { 	"Get coffee with milk" + addLike,
			"Get coffee with sugar",
			"Poison coffee" + addLP};  
		
		quest [1] = new dialogueSegment ("What do you want to do?", 
		                                 3, opts, 
		                                 0, null);
	}
}
