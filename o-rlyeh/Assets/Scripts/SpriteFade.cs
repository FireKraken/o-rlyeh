using UnityEngine;
using System.Collections;

public class SpriteFade : MonoBehaviour {

	// END OF DAY FADE IO STUFF
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	public bool dialogueEnding = false;      // Whether or not the scene is still fading in.
	public bool dialogueStarting  = false ; 
	public Color col; 

	public SpriteRenderer renderer;  

	void Start () {
		renderer = GetComponent<SpriteRenderer> (); 
		col = renderer.color; 

		renderer.enabled = false; 

		dialogueEnding = true;
		dialogueStarting = false; 
	}

	void Update () {
		beginDialogue (); 
		endDialogue (); 
	}

	private void beginDialogue()
	{
		if (dialogueStarting)
		{
			renderer.enabled = true;
			FadeIn (); 
		}
	}
	private void endDialogue()
	{
		if(dialogueEnding)
		{
			FadeOut();
		}
	}
	
	void FadeToClear ()
	{
		// Lerp the colour of the texture between itself and transparent.
		renderer.color = Color.Lerp(renderer.color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	
	void FadeToSprite ()
	{
		// Lerp the colour of the texture between itself and black.
		renderer.color = Color.Lerp(renderer.color, col, fadeSpeed * Time.deltaTime);
	
	}
	
	
	void FadeOut ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(renderer.color.a <= 0.05f)
		{			
			renderer.color = Color.clear; 
			renderer.enabled = false; 
			// The scene is no longer starting.  
			dialogueEnding = false; 
		}
	}
	
	
	public void FadeIn ()
	{		
		// Start fading towards black.
		FadeToSprite();
		
		// If the screen is almost black...
		if (renderer.color.a >= 0.95f)
		{
			renderer.color = col; 
			dialogueStarting = false; 
		}
	}

}
