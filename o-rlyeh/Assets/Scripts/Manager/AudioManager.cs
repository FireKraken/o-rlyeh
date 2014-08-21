using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	static public AudioManager ins;

	public CameraZoom camZoom ;
	public AudioClip[] zoomClips; 

	public void Initialize () {
		ins = this;
		this.gameObject.name = "AudioManager";
		DontDestroyOnLoad(this); 
	}

	public void PlayClip(){
		if (GameManager.ins.status == GameState.GameStatus.Game){
			if (camZoom.currentState == (int) CameraZoom.camView.space){
				audio.clip = zoomClips[1];
			}
			else {
				audio.clip = zoomClips[0]; 
			}
		}
		audio.Play (); 
	}

	void Update () {
		//if (GameManager.ins.status == (int) GameState.GameStatus.End){
		//	Destroy (this.gameObject);
		//}
	}
}
