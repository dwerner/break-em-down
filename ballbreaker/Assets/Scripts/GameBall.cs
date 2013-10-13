using UnityEngine;
using System.Collections;

public class GameBall : MonoBehaviour {
   public float minSpeed = 10;
   public float maxSpeed = 20;
   public float curSpeed = 10;
   public Score score;

   public AudioClip collisionSound;

   void Start() {
      rigidbody.AddRelativeForce(new Vector3(0, this.minSpeed, 0));
   }

   void Update() {
	
   }

   void FixedUpdate() {
      this.curSpeed = Vector3.Magnitude(rigidbody.velocity);
		
      if (this.curSpeed > this.maxSpeed) {
         rigidbody.velocity = (rigidbody.velocity) / (this.curSpeed / this.maxSpeed);
      }
      else if (this.curSpeed < this.minSpeed) {
         rigidbody.velocity = (rigidbody.velocity) / (this.curSpeed / this.minSpeed);
      }
   }

   void OnCollisionExit (Collision c) {

      if (this.collisionSound != null){
         audio.PlayOneShot(this.collisionSound);
      }

   }

   void OnCollisionEnter(Collision col) {
      Debug.Log("Collided witdddh " + col.gameObject.name);
      score.increaseBy(10);
   }
}
