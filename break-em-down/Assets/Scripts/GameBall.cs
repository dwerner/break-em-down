﻿using System; 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameBall : LevelObject {

  
   public float angleThreshold = 10.0f;
   public bool pulseOnImpact = false;
   public float pulseAmount = 1.4f;
   public AudioClip collisionSound;

    private bool playStopped = false;

   private float[] pulseFrames;
   private bool isPulsing = false; //concurrent lock (possible/atomic because concurrency is done via co-routines)
   public float speed = 200f;

   void Awake(){
      this.pulseFrames = this.buildPulseFrames();


   }

   private Transform paddleTransform;

   IEnumerator Start() {
 
      if (paddleTransform == null){
        paddleTransform = FindObjectOfType<PlayerPaddle>().transform;
      }

      this.levelController.BallOutOfBounds += (object sender) => {
      if (!playStopped){
          var paddle = paddleTransform;
          this.transform.parent = paddle;
          this.transform.position = new Vector3(paddle.position.x, paddle.position.y + 1f, 0.5f);

          Debug.Log("repositioned and re-parented ball"+ this.enabled);
        }
      };

    this.levelController.GameOver += (sender, e) => { this.stop(); };

    this.levelController.LevelWon += (sender, e) => { this.stop(); };

      this.levelController.StartPlay += (object sender) => {
        if (!playStopped) {
          this.transform.parent = levelController.transform;
          this.go();
        }
      };

      while (true) {

         if (this.transform.position.magnitude > 50.0f){
            levelController.RaiseBallOutOfBounds();
         }

         yield return null;
      }
   }

  void stop(){
    this.rigidbody.velocity = new Vector3(0,0,0);
    playStopped = true;
  }
  
  void FixedUpdate() {
      if (transform.parent == paddleTransform){
        this.rigidbody.velocity = new Vector3(0f,0f,0f);
      }
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

   private void go(){
      if (rigidbody.velocity.magnitude <= 0.5){
        rigidbody.AddRelativeForce( new Vector3(0, speed*2) );
      }
   }


   IEnumerator OnCollisionExit (Collision c) {

      if (this.collisionSound != null){
         audio.PlayOneShot(this.collisionSound);

      }

      Debug.DrawRay(rigidbody.position, rigidbody.velocity, Color.black, 2, false);
      Debug.DrawRay(c.contacts[0].point, this.rigidbody.velocity, Color.green, 2, false);
      
      // x bump
      if (Math.Abs(rigidbody.velocity.x) < angleThreshold){
        rigidbody.AddRelativeForce(new Vector3(3 * (rigidbody.velocity.x>0?1:-1), 0));

        Debug.DrawRay(rigidbody.position, rigidbody.velocity, Color.red, 2, false);

      }

      // y bump - prevent locking on a single axis
      if (Math.Abs(rigidbody.velocity.y) < angleThreshold){
        rigidbody.AddRelativeForce(new Vector3(0, 3 * (rigidbody.velocity.y>0?1:-1)));

        Debug.DrawRay(rigidbody.position, rigidbody.velocity, Color.blue, 2, false);

      }


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
