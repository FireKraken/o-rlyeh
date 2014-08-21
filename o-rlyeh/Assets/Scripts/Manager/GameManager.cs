using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// this instance of the GameManager
	static public GameManager ins; 

	// global variables
	public GameState.GameStatus status; 

	// game objects to instantiate
	public GameObject audioManager;

	// components in manager to enable/disable depending on game status
	public CameraZoom camZoom;

	void Awake(){
		DontDestroyOnLoad(this);

		if (GameObject.Find ("GameManager") != null)
		{
			Destroy (this.gameObject);
		}

		ins = this;
		GetStatus (); 

		if (AudioManager.ins == null)
		{
			(Instantiate(audioManager) as GameObject).SendMessage ("Initialize");
			AudioManager.ins.camZoom = gameObject.GetComponent<CameraZoom>();
			AudioManager.ins.SendMessage ("PlayClip");
		}

		camZoom = gameObject.GetComponent<CameraZoom>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetStatus ();
	}

	private void GetStatus(){
		if (Application.loadedLevelName.Equals ("TitleScreen")){
			status = GameState.GameStatus.Splash; 
		}
		else if (Application.loadedLevelName.Equals("Intro")){
			status = GameState.GameStatus.Intro;
		}
		else if (Application.loadedLevelName.Equals ("Main")){
			status = GameState.GameStatus.Game;
		}
	}
}
