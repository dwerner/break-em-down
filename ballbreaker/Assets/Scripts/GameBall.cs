using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameBall : MonoBehaviour {

   public float minSpeed = 10;
   public float maxSpeed = 20;
   public float curSpeed = 10;
   public Score score;
   public bool pulseOnImpact = false;
   public float pulseAmount = 1.4f;
   public AudioClip collisionSound;

   public LevelController levelController;

   private float[] pulseFrames;
   private bool isPulsing = false; //concurrent lock (possible/atomic because concurrency is done via co-routines)


   void Awake(){
      this.pulseFrames = this.buildPulseFrames();

   }

   void Start() {
      rigidbody.AddRelativeForce(new Vector3(0, this.minSpeed, 0));
 
   }


   /*
    * build an array like [1.0f, 1.1f, 1.2f ... <pulseAmount> ... 1.1f, 1.0f]
    * 
    * this is used later to scale the object a certain about per frame in Pulse()
    */
   private float[] buildPulseFrames () {

      var frames = new List<float>();

      for (float i = 1.0f; i <= this.pulseAmount; i += 0.1f) {
         frames.Add(i);
      
      }

      for (float i = this.pulseAmount; i >= 1.0f; i -= 0.1f) {
         frames.Add(i);
      
      }

      return frames.ToArray();

   }


   void FixedUpdate() {
      this.curSpeed = Vector3.Magnitude(rigidbody.velocity);
		
      if (this.curSpeed > this.maxSpeed) {

         rigidbody.velocity /= this.curSpeed / this.maxSpeed;

      }
      else if (this.curSpeed < this.minSpeed && this.curSpeed != 0) {

         rigidbody.velocity /= this.curSpeed / this.minSpeed;

      }
   }


   IEnumerator OnCollisionExit (Collision c) {

      if (this.collisionSound != null){
         audio.PlayOneShot(this.collisionSound);
      }

      if (this.pulseOnImpact) {
         yield return StartCoroutine(this.Pulse());
      }

      yield return null;
   }


   void OnCollisionEnter(Collision col) {

      Debug.Log("Collided with " + col.gameObject.name);

 
      #region collision_logic // what do we do when we collide with x

      if (col.gameObject.GetComponent<OutOfBounds>() != null) {

         Debug.Log("ball lost");
         this.gameObject.SetActive(false); // no longer active if we are out of bounds
         return;

      }

      if (col.gameObject.GetComponent<Brick>() != null){

         if (this.score != null) {

            score.increaseBy(10);
         
         }
      }

      #endregion
   }



   /*
    * Cause this object to pulse
    */
   IEnumerator Pulse(){

      if (!this.isPulsing) {

         this.isPulsing = true;
         var originalLocalScale = this.transform.localScale;

         foreach (var f in this.pulseFrames) {
      
            this.transform.localScale = originalLocalScale * f;

            yield return null;

         }

         this.transform.localScale = originalLocalScale;
         this.isPulsing = false;
      }

      yield return null;
   }

}
