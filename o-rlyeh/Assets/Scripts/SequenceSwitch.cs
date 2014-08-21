using UnityEngine;
using System.Collections;

public class SequenceSwitch : MonoBehaviour {
	
	private int id; 
	public GameObject nextScreen; 
	public string nextScene; 
	public bool switched; 

	private static int current;
	// Use this for initialization
	void Start () {
		nextScene = "Main";
		id = 0;
		current = 1;
		getID ();
		if (id != 16){
			nextScreen = GameObject.Find ("Intro" + (id+1));
			nextScreen.GetComponent<SpriteRenderer>().enabled = false;
		}
		switched = false; 
		if (current != id){
			this.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1))
		{
			Application.LoadLevel (nextScene); 
		}
		else if (Input.anyKeyDown) 
		{
			if (current == 16) 
			{
				Application.LoadLevel (nextScene); 
			}
			else {
				nextScreen.GetComponent<SpriteRenderer>().enabled = true;
				switched = true; 
				current++;
			}
		}

		if (switched) {
			nextScreen.GetComponent<SequenceSwitch>().enabled = true;
			gameObject.SetActive (false); 
		}
	}

	private void getID(){
		for (int i = 0; id == 0; i++){
			if (gameObject.name.Equals("Intro"+i)){
				id = i;
			}
		}
	}
}
