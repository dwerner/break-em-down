using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuBall : MonoBehaviour {
	
	public float minimumSpeed = 5f;
	public float maxSpeed = 25f;
	public float speed = 0f;
	
	public AudioClip collisionSound;

	// Use this for initialization
	void Start () {
		this.rigidbody.AddRelativeForce(new Vector3(0,this.maxSpeed,0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FixedUpdate(){
		this.speed = Vector3.Magnitude(rigidbody.velocity);
		
		if (this.speed > this.maxSpeed){
			this.rigidbody.velocity /= this.speed / this.maxSpeed;
			
		} else if (this.speed < this.minimumSpeed && this.speed > 0){
			this.rigidbody.velocity /= this.speed / this.minimumSpeed;
		
		} 
	}
	
	void OnCollisionExit (Collision c) {
		
		if (this.collisionSound != null){
			audio.PlayOneShot(this.collisionSound);
		}
		
	}
	/* // This was just a trial thing anyway - perhaps we could make this scale-independent
	IEnumerator OnCollisionEnter (Collision c){
		var sizes = new List<float>() {
		1.0f,1.1f,1.2f,1.3f,1.2f,1.1f,1.0f
		};
		
		foreach (var i in sizes){
			transform.localScale = new Vector3(i,i,i);
			yield return 0;
		}
	}
	*/
}
