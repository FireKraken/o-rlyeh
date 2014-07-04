using UnityEngine;
using System.Collections;

public class SequenceSwitch : MonoBehaviour {

	private SequenceFade sf;
	public GameObject nextScreen; 
	public bool lastScreen; 
	public string nextScene; 
	public bool switched; 
	// Use this for initialization
	void Start () {
		sf = GetComponent<SequenceFade> (); 
		sf.dialogueStarting = true; 
		switched = false; 
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) 
		{
			if (lastScreen) 
			{
				Application.LoadLevel (nextScene); 
			}
			nextScreen.SetActive (true); 
			sf.dialogueEnding = true;
			switched = true; 
		}

		if (switched) {
			if (!sf.dialogueEnding){
				gameObject.SetActive (false); 
			}
		}
	}
}
