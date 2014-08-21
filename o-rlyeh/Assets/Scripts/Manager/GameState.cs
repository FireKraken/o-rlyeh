using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	//GAME STATES
	public enum GameStatus { Splash, Intro, Tutorial, Game, Paused, End }
	
	void Awake () {
		DontDestroyOnLoad(this);
	}

	void Start () {
		if (GameObject.Find ("GameState") == null)
		{
			this.gameObject.name = "GameState";
		}
		else 
		{
			Destroy (this.gameObject);
		}
	}
}
