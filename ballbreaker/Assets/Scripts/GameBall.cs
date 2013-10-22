using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameBall : MonoBehaviour {


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

      rigidbody.AddRelativeForce(new Vector3(150.0f, 150.0f));
   }

   void FixedUpdate() {

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
  

   IEnumerator OnCollisionExit (Collision c) {

      if (this.collisionSound != null){
         audio.PlayOneShot(this.collisionSound);
      }

      Debug.DrawRay(rigidbody.position, rigidbody.velocity, Color.black, 2, false);
      Debug.DrawRay(c.contacts[0].point, this.rigidbody.velocity, Color.green, 2, false);


      if (this.pulseOnImpact) {
         yield return StartCoroutine(this.Pulse());
      }

      yield return null;
   }


   void OnCollisionEnter(Collision c) {
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
