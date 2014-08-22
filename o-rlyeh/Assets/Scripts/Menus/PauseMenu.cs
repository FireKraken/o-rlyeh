using UnityEngine;
using System.Collections;

public class PauseMenu : Menu {

	public Texture2D bgTexture;
	public Color bgColour; 
	public GUISkin skin;

	private int idx = 0;
	private float timer = 0;

	private bool wasDown = false;
	private bool wasUp = false;

	private Item[] items = new Item[]{
		new Item("resume game", delegate () { noPause(); }),
		new Item("restart level", delegate() { Application.LoadLevel(Application.loadedLevel);}),
		new Item("options", delegate () { }), //modifyOptions(); }),
		new Item("quit", delegate () { Application.Quit(); })
	};

	void OnGUI () {
		GUI.skin = skin;
		GUI.color = bgColour;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), bgTexture);
		GUI.color = Color.white;
		GUIMenu(idx, 200, 80, items, timer);
	}

	public static void noPause(){
	}

	void Update () {
		print (items[0]);
		timer += Time.deltaTime;
		
		bool 	isUp = Input.GetAxis("Vertical") > 0.8f,
				isDown = Input.GetAxis("Vertical") < -0.8f;
		bool 	justUp = isUp && !wasUp,
				justDown = isDown && !wasDown;
		
		if (Input.GetKeyDown("Down") || justDown) {
			Debug.Log("d");
			idx += 1;
			idx %= items.Length;
			timer = 0;
		}
		if (Input.GetKeyDown("Up") || justUp) {
			Debug.Log("u");
			idx += items.Length - 1;
			idx %= items.Length;
			timer = 0;
		}
		
		if (Input.GetKeyDown("Confirm")) {
			Debug.Log("conf");
			items[idx].command();
		}
		
		if (Input.GetKeyDown("Cancel")) {
			Debug.Log("canc");
			//GameManager.ins.UnPause();
		}
		
		wasUp = isUp;
		wasDown = isDown;
	}
}
