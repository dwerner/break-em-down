using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class PlayerPaddle : LevelObject {
   public float speed = 20;

   public float leftLimit = -7.5f;

   public float rightLimit = 7.5f;

   public bool touchesExist;

   private TKPanRecognizer panner;
   private TKRotationRecognizer rotater;
   private TKTapRecognizer tapper;


   //Changing this into an IEnumerator - so re-entry happens once per frame, and Update is never needed
   IEnumerator Start() {
      this.setupGestures();

      yield return StartCoroutine(this.GameLoop());
   }

   void setupGestures() {

      this.panner = new TKPanRecognizer();
      this.rotater = new TKRotationRecognizer();
      this.tapper = new TKTapRecognizer();

      // when using in conjunction with a pinch or rotation recognizer setting the min touches to 2 smoothes movement greatly
      if (Application.platform == RuntimePlatform.IPhonePlayer){
         this.panner.minimumNumberOfTouches = 2;
      }

      this.panner.gestureRecognizedEvent += this.movePaddle;
      this.rotater.gestureRecognizedEvent += this.rotatePaddle;
      this.tapper.gestureRecognizedEvent += this.startBallEvent;

      TouchKit.addGestureRecognizer(this.panner);
      TouchKit.addGestureRecognizer(this.rotater);
      TouchKit.addGestureRecognizer(this.tapper);

   }

   void movePaddle(TKPanRecognizer r){
      transform.position += new Vector3(r.deltaTranslation.x, 0) / 50;

   }

   void rotatePaddle(TKRotationRecognizer r){
      transform.Rotate(Vector3.up, r.deltaRotation);

   }

   void startBallEvent(TKTapRecognizer r){
     this.startBall();
   }

   void startBall() {
     this.levelController.RaiseStartPlay();
   }


   void OnDestroy() {

      this.panner.gestureRecognizedEvent -= this.movePaddle;
      this.rotater.gestureRecognizedEvent -= this.rotatePaddle;
      this.tapper.gestureRecognizedEvent -= this.startBallEvent;

      TouchKit.removeAllGestureRecognizers();

   }

   IEnumerator GameLoop() {

      while (true) {
         var h = Input.GetAxis("Horizontal");
         var space = Input.GetKey(KeyCode.Space);
         if (space){
            this.startBall();
         }

         if (h != 0) {
            var move = h * Time.deltaTime * speed; // poop

            /* 
             * For the paddle, perhaps it does make more sense to control via position, and limit based on a static width.
             * The alternative is raycasting, and predicting the movement of the object prior to it moving, and stopping it.
             * 
             * Otherwise we just get intersections, and they don't feel very realistic. 
             * 
             * *Note that changing the masses of the targets did have an effect on the kinds of intersections that were allowed before the object rebounds.
             */
            if (Mathf.Abs(h) > 0) {
               if (!(this.transform.position.x + move < this.leftLimit) &&
                     !(this.transform.position.x + move > this.rightLimit)) { 

                  this.transform.position += new Vector3(move, 0.0f, 0.0f);
               } 
            }

         }

         yield return null;
      }
   }
}


