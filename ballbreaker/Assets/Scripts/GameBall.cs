using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameBall : MonoBehaviour {

   public float speed = 0.1f;
   private Vector3 direction;

   public float bounceVariance = 1.0f;

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
      this.direction = new Vector3(1.0f, 1.0f).normalized;
   }

   void FixedUpdate() {
      this.direction = this.direction.normalized;
      this.rigidbody.velocity = direction * speed;
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




      if (this.pulseOnImpact) {
         yield return StartCoroutine(this.Pulse());
      }

      yield return null;
   }


   void OnCollisionEnter(Collision col) {

      var seed = (this.bounceVariance * Random.value)/2; //introduce a little randomness into the angles of reflection

      Debug.Log("Collided with " + col.gameObject.name);

      var contact = col.contacts[0];
      Debug.DrawRay(contact.point, contact.normal, Color.green, 2, false);
      Debug.DrawRay(contact.point, direction, Color.red, 2, false);

      var reflected = Vector3.Reflect(this.direction, contact.normal);
      this.direction = new Vector3(reflected.x + (seed-(seed/2)), reflected.y+(seed-(seed/2)));
      Debug.Log("reflected ball");
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
