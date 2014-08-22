using UnityEngine;
using System.Collections.Generic;

public class InGame : MonoBehaviour {
	
	public bool paused {
		set {
			// freeze everything!
			_paused = value;
		}
		get { return _paused; }
	}
	private bool _paused = false;
	
	void Update () {
		if (!paused) {
			GameUpdate();
		}
	}
	void LateUpdate () {
		if (!paused) {
			GameLateUpdate();
		}
	}
	public virtual void GameUpdate () {
		// unfreeze!
	}
	
	public virtual void GameLateUpdate () {
		// unfreeze!
	}
	
	private void UpdateComponentStatus<T> (bool paused, ref bool status) {
		
	}
}