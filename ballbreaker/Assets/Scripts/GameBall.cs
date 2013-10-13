using UnityEngine;
using System.Collections;

public class GameBall : MonoBehaviour {
	
   public float minSpeed = 10;
   public float maxSpeed = 20;
   public float curSpeed = 10;
   // Use this for initialization
   void Start () {
      Debug.Log ("Test");
      rigidbody.AddRelativeForce (new Vector3 (0, this.minSpeed, 0));
   }
   // Update is called once per frame
   void Update () {
	
   }

   void FixedUpdate () {
      this.curSpeed = Vector3.Magnitude (rigidbody.velocity);
		
      if (this.curSpeed > this.maxSpeed) {
         rigidbody.velocity = (rigidbody.velocity) / (this.curSpeed / this.maxSpeed);
      }
      else if (this.curSpeed < this.minSpeed) {
         rigidbody.velocity = (rigidbody.velocity) / (this.curSpeed / this.minSpeed);
      }
   }

   void OnCollisionEnter (Collision col) {
      Debug.Log ("Collided witdddh " + col.gameObject.name);
      Score.score += 10;
   }
}
