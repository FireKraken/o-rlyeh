using UnityEngine;
using System.Collections;

public class BGScrolling : MonoBehaviour {

	public float scrollSpeed; 
	public float delay;
	public float createTime; 
	public float offsetX; 
	public float replaceX; 


	public GameObject currentBG; 
	private Vector3 startPosition; 


	void Start () {
		startPosition = transform.position; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		transform.position = new Vector3 (transform.position.x - scrollSpeed, transform.position.y, transform.position.z);
		if (transform.position.x <= startPosition.x - offsetX) {
			transform.position = new Vector3 (startPosition.x + replaceX, transform.position.y, transform.position.z);
		}

	}
}
