using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerPaddle : MonoBehaviour {
	
	public float speed = 50; 
	/*
	public static float zPos; // numbers always initialize to 0 - not like in C
	public float yPos;
	public float xPos;
	
	public float xBoundLeft; // i edited the properties in the inspector but they did not carry over here
	public float xBoundRight; // when do they get set? <- in edit mode, but no initialization is needed. if you edit in the 
	public float yBoundTop; // 
	*/
	private Transform _t;
/*	
	void Awake() {
		_t = transform; // This is silly. I know the guy was doing it in the tut, but there's no property; this.transform is a field. No optimization to be had here.
	}
*/
	//Changing this into an IEnumerator - so re-entry happens once per frame, and Update is never needed
	IEnumerator Start () {
		float velocity = 0.0f;
		while (true) {
			var h = Input.GetAxis("Horizontal");

			if (h != 0) {
				Debug.Log (h);
				this.rigidbody.AddRelativeForce (new Vector3 (h, 0.0f, 0.0f) * Time.deltaTime * speed); 
			} else {
				this.rigidbody.AddForce(this.rigidbody.velocity * -1);
			}
			yield return null;
		}
	}
	
}
