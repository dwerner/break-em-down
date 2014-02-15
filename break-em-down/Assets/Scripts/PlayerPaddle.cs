using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class PlayerPaddle : LevelObject {
   public float speed = 20;

   public float leftLimit = -7.5f;

   public float rightLimit = 7.5f;

   Quaternion initialRotation;

   public bool touchesExist;

   private TKPanRecognizer panner;
   private TKRotationRecognizer rotater;
   private TKTapRecognizer tapper;


   //Changing this into an IEnumerator - so re-entry happens once per frame, and Update is never needed
   IEnumerator Start() {
      this.setupGestures();

      initialRotation = this.transform.rotation;

      levelController.BeforeBallOutOfBounds += (object sender) => this.ResetRotation();

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
      
      //transform.position += new Vector3(r.deltaTranslation.x, 0) / 50;
      movePaddle(r.deltaTranslation.x/50);
   }

  //left is -3 right is 3 
   void movePaddle(float move){
    if (this.transform.position.x + move >= this.leftLimit && this.transform.position.x + move <= this.rightLimit) { 
        this.transform.position += new Vector3(move, 0.0f, 0.0f);
    
    }

   }

   void rotatePaddle(TKRotationRecognizer r){
    rotatePaddle(r.deltaRotation);   

   }

   public float minimumRotation;
   public float maximumRotation;

   void rotatePaddle(float d){

    var zAngle = transform.rotation.eulerAngles.z;
    var zRotation = zAngle+d;

    if (zRotation >= minimumRotation && zRotation <= maximumRotation){
      transform.Rotate(Vector3.forward, d, Space.World);
    
    }

   }

   void startBallEvent(TKTapRecognizer r){
     
     this.startBall();
   }

   void startBall() {
     this.levelController.RaiseStartPlay();
   }

  void ResetRotation(){
    this.transform.rotation = this.initialRotation;
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
         var scrollWheel = Input.GetAxis("Mouse ScrollWheel");
         var space = Input.GetKey(KeyCode.Space);
         if (space){
            this.startBall();
         }

         if (scrollWheel > 0){
           rotatePaddle( 5 );
         }
         if (scrollWheel < 0){
           rotatePaddle( -5 );
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

              movePaddle(move);
            }
         }

         yield return null;
      }
   }
}


