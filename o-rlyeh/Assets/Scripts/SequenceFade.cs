using UnityEngine;
using System.Collections;

public class SequenceFade : MonoBehaviour {
	
	// END OF DAY FADE IO STUFF
	public float fadeSpeed = 1.5f;          // Speed that the screen fades to and from black.
	public bool dialogueEnding = false;      // Whether or not the scene is still fading in.
	public bool dialogueStarting  = false ; 
	public Color col; 
	
	public SpriteRenderer _renderer;  
	
	void Start () {
		_renderer = GetComponent<SpriteRenderer> (); 
		col = _renderer.color; 
		
		_renderer.enabled = false; 
	}
	
	void Update () {
		beginDialogue (); 
		endDialogue (); 
	}
	
	private void beginDialogue()
	{
		if (dialogueStarting)
		{
			_renderer.enabled = true;
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
		_renderer.color = Color.Lerp(_renderer.color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	
	void FadeToSprite ()
	{
		// Lerp the colour of the texture between itself and black.
		_renderer.color = Color.Lerp(_renderer.color, col, fadeSpeed * Time.deltaTime);
		
	}
	
	
	void FadeOut ()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
		if(_renderer.color.a <= 0.05f)
		{			
			_renderer.color = Color.clear; 
			_renderer.enabled = false; 
			// The scene is no longer starting.  
			dialogueEnding = false; 
		}
	}
	
	
	public void FadeIn ()
	{		
		// Start fading towards black.
		FadeToSprite();
		
		// If the screen is almost black...
		if (_renderer.color.a >= 0.95f)
		{
			_renderer.color = col; 
			dialogueStarting = false; 
		}
	}
	
}
