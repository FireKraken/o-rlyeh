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

	// END OF DAY FADE IO STUFF
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	public bool dayStarting = false;      // Whether or not the scene is still fading in.
	public bool endTheDay  = false ; 

	void Awake()
	{
		// Set the texture so that it is the the size of the screen and covers it.
		guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
	}
	
	void Start () 
	{
		
	}

	void Update () 
	{

		// go to sleep and wake up
		if (endTheDay) {
			guiTexture.enabled = true;	
			endDay (); 
		}

		// if did both days
		if (day > numDays) 
		{
			generateEnding (); 
		}
	}


	/* --------------------------------------------------------------------------------------------------------------------------
	 * NO ARGS. NO RETURN. 
	 * SLEEP = FADE OUT TO BLACK, THEN FADE BACK INTO SAME SCENE
	 * -------------------------------------------------------------------------------------------------------------------------- */
	private void endDay()
	{
		// If the scene is starting...
		if(dayStarting)
		{
			// ... call the StartScene function.
			StartScene();
		}
		else 
		{
			// Make sure the texture is enabled.
			guiTexture.enabled = true;
			EndScene (); 
		}
	}
	
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	
	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	
	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(guiTexture.color.a <= 0.05f)
		{
			// ... set the colour to clear and disable the GUITexture.
			guiTexture.color = Color.clear;
			guiTexture.enabled = false;
			
			// The scene is no longer starting.
			dayStarting = false;
			endTheDay = false; 
		}
	}
	
	
	public void EndScene ()
	{		
		// Start fading towards black.
		FadeToBlack();

		// If the screen is almost black...
		if (guiTexture.color.a >= 0.95f)
		{
			guiTexture.color = Color.black;

			dayStarting = true; 
		}
	}
	
	/* --------------------------------------------------------------------------------------------------------------------------
	 * NO ARGS. NO RETURN. 
	 * if surpassed total number of days (i.e. exiting wormhole), generate one of currently 4 endings
	 * -------------------------------------------------------------------------------------------------------------------------- */
	private void generateEnding()
	{
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
