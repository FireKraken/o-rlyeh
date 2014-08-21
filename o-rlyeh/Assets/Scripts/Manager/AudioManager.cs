using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	static public AudioManager ins;

	public CameraZoom camZoom ;

	// AUDIOCLIPS
	public AudioClip titleClip; 
	public AudioClip[] zoomClips; 

	public void Initialize () {
		ins = this;
		this.gameObject.name = "AudioManager";
		DontDestroyOnLoad(this); 
	}

	public void PlayClip(){
		audio.Play (); 
	}

	void Update () {
		//if (GameManager.ins.status == (int) GameState.GameStatus.End){
		//	Destroy (this.gameObject);
		//}
		GetClip ();
	}

	public void GetClip(){
		if (GameManager.ins.status == GameState.GameStatus.Splash){
			if (audio.clip != titleClip){
				audio.clip = titleClip; 
			}
		}
		else if (GameManager.ins.status == GameState.GameStatus.Game){
			if (camZoom.currentState == (int) CameraZoom.camView.space){
				if (audio.clip != zoomClips[1]){
					audio.clip = zoomClips[1];
				}
			}
			else {
				if (audio.clip != zoomClips[0]){
					audio.clip = zoomClips[0]; 
				}
			}
		}
	}
}
