using UnityEngine;
using System.Collections;

public class PlayerTrigger : MonoBehaviour 
{
	// CAMERA
	public Camera mainCam;
	public SpriteFade sf; 
	
	// ZOOM IN/OUT STUFF
	public float camSizeBridge = 1.5f;
	public float camSizeOthers = 1.25f; 
	public float camSizeExit = 5.0f;

	public Vector3 curPos; 
	private float t;

	// LERP STUFF
	public Vector3 newPos;
	public float newSize;
	public float smooth = 0.5f;

	// Checks for whether a dialogue has been completed
	public bool captDialogue1 = true;
	public bool pugDialogue1 = false;
	public bool captDialogue2 = false;
	public bool pugDialogue2 = false;

	// Prevent player movement when action is paused
	public bool pauseAction = false;
	public bool promptVisible = false;
	public Vector3[] buttonPos; 
	private enum iconPos  { cpt, pug, generator }; 

	// interaction button 
	public GameObject buttonPrompt;
	public Sprite[] buttons; 
	private enum icon { e, i, t }; 

	// generating pugs
	public GameObject pug;
	public Vector3 pugPos; 
	public int pugCt; 
	public float minPugForce;
	public float maxPugForce;

	// Use this for initialization
	void Start () 
	{		
		newPos = mainCam.transform.position;
		newSize = mainCam.orthographicSize; 
		pugCt = 0; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (pauseAction) 
		{
			// Pause the action
			rigidbody2D.isKinematic = true;
			// mainCam.orthographicSize = 5.0f;
		} 
		else 
		{
			// Un-pause the action
			rigidbody2D.isKinematic = false;
			rigidbody2D.WakeUp();
		}

		if (promptVisible)
		{
			buttonPrompt.renderer.enabled = true;
		}
		else
		{
			buttonPrompt.renderer.enabled = false;
		}
	}

	void FixedUpdate(){
		if (!GameManager.ins.gameObject.GetComponent<CameraZoom>().lerping){
			mainCam.orthographicSize = Mathf.Lerp (mainCam.orthographicSize, newSize, smooth * Time.deltaTime);
			mainCam.transform.position = Vector3.Slerp (mainCam.transform.position, newPos, smooth * Time.deltaTime);
		}
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		curPos = new Vector3 (mainCam.transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z);
		
		if (other.tag == "MessHall")
		{
			newSize = 1.25f;
			newPos = new Vector3 (-0.575f, 0.175f, curPos.z);
			Debug.Log ("Entered mess hall.");
		}
		else if (other.tag == "EngineRoom")
		{
			newSize = 1.25f;
			newPos = new Vector3 (-0.465f, -2.175f, curPos.z);
			Debug.Log ("Entered engine room.");
		}
		else if (other.tag == "CrewQuarters")
		{
			newSize = 1.25f;
			newPos = new Vector3 (-5.0f, 0.175f, curPos.z);
			Debug.Log ("Entered crew quarters.");
		}
		else if (other.tag == "ObservationDeck")
		{
			newSize = 1.25f;
			newPos = new Vector3 (0.0f, 2.5f, curPos.z);
			Debug.Log ("Entered observation deck.");
		}
		else if (other.tag == "Bridge")
		{
			newSize = 1.5f;
			newPos = new Vector3 (4.275f, 0.1f, curPos.z);
			Debug.Log ("Entered bridge.");
		}
		curPos = mainCam.transform.position; 
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.tag == "CaptainObjective" && (captDialogue1 || captDialogue2))
		{
			Debug.Log ("Entered Captain's conversation trigger.");
			buttonPrompt.GetComponent<SpriteRenderer>().sprite = buttons[(int) icon.t]; 
			buttonPrompt.transform.position = buttonPos [(int) iconPos.cpt];
			promptVisible = true;

			if (Input.GetKeyDown (KeyCode.T))
			{
				pauseAction = true;
				promptVisible = false;
				captDialogue1 = false;
				sf.dialogueStarting = true;
			}
		}
		else if (other.tag == "PugGenerator")
		{
			if (pugCt < 50){
				Debug.Log ("Entered Pug Generator trigger.");
				buttonPrompt.GetComponent<SpriteRenderer>().sprite = buttons[(int) icon.e]; 
				buttonPrompt.transform.position = buttonPos [(int) iconPos.generator];
				promptVisible = true;
				
				if (Input.GetKeyDown (KeyCode.E))
				{
					pugCt++; 
					GameObject p = Instantiate (pug, pugPos, new Quaternion (0, 0, 0, 0)) as GameObject;
					Vector2[] dir = new Vector2[2] { Vector2.up, -Vector2.right };
					p.rigidbody2D.AddForce (Random.Range (minPugForce, maxPugForce) * dir[Random.Range(0, dir.Length)]);
					p.name = "Pug " + pugCt; 
				}
			}
			else {
				promptVisible = false;
			}
		}
	}
	
	void OnTriggerExit2D (Collider2D other)
	{
		curPos = new Vector3 (mainCam.transform.position.x, mainCam.transform.position.y, mainCam.transform.position.z);
		
		if (other.tag == "MessHall" || other.tag == "EngineRoom" || other.tag == "CrewQuarters" || other.tag == "ObservationDeck" || other.tag == "Bridge")
		{
			newSize = 5.0f;
			newPos = new Vector3 (-0.575f, 0.175f, curPos.z);
			Debug.Log ("Moving through the ship.");
		}
		curPos = mainCam.transform.position; 

		if (other.tag == "CaptainObjective" && (captDialogue1 || captDialogue2))
		{
			promptVisible = false;
		}
		if (other.tag == "PugGenerator"){
			promptVisible = false;
		}
	}
}