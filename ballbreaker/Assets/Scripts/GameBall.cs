using UnityEngine;
using System.Collections;

public class GameBall : MonoBehaviour {
	
	public float minSpeed = 15;
	public float maxSpeed = 30;
	public float curSpeed = 15;

	// Use this for initialization
	void Start () {
		rigidbody.AddRelativeForce(new Vector3(0, this.minSpeed, 0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate() {
	}
	
	void OnCollisionEnter(Collision col) {
		Debug.Log("Collided with " + col.gameObject.name);
	}
}
