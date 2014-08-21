using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// this instance of the GameManager
	static public GameManager ins; 

	// global variables
	public GameState.GameStatus status; 

	// game objects to instantiate
	public GameObject audioManager;
	public GameObject mainMenu; 

	// components in manager to enable/disable depending on game status
	public CameraZoom camZoom;

	void Awake(){
		DontDestroyOnLoad(this);

		if (GameObject.Find ("GameManager") != null)
		{
			Destroy (this.gameObject);
		}

		ins = this;

		/*if (AudioManager.ins == null)
		{
			(Instantiate(audioManager) as GameObject.SendMessage ("Initialize"));
		}*/
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
