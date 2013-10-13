using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class PlayerPaddle : MonoBehaviour {
	public float speed = 20;
	public float leftLimit = -7.5f;
	public float rightLimit = 7.5f;
	public bool touchesExist;
	

   //Changing this into an IEnumerator - so re-entry happens once per frame, and Update is never needed
	IEnumerator Start () {


      yield return StartCoroutine(this.GameLoop());
	}

   IEnumerator GameLoop(){
      Vector3 pos = rigidbody.position;
      while (true) {
         var h = Input.GetAxis ("Horizontal");

         if (h != 0) {
            var move = h * Time.deltaTime * speed;

            /* 
             * For the paddle, perhaps it does make more sense to control via position, and limit based on a static width.
             * The alternative is raycasting, and predicting the movement of the object prior to it moving, and stopping it.
             * 
             * Otherwise we just get intersections, and they don't feel very realistic. 
             * 
             * *Note that changing the masses of the targets did have an effect on the kinds of intersections that were allowed before the object rebounds.
             */
            if (Mathf.Abs (h) > 0) {
               if (!(this.rigidbody.position.x + move < this.leftLimit) &&
                   !(this.rigidbody.position.x + move > this.rightLimit)) { 

                  this.rigidbody.position += new Vector3 (move, 0.0f, 0.0f);
               }
            }
         }
         else {
            this.rigidbody.velocity *= 0;
         }


         /*
          * Bug here: the values from Input.touches seem to be 1:1 with device pixels
          * So the paddle moves with an offset of 1/2 the x+y resolution.
          * 
          */
         if (Input.touches.Count () > 0) {
            var delta = Input.touches[0].position;

            var movingTouches = from x in Input.touches
                                where x.phase == TouchPhase.Moved
                                select x; 



            rigidbody.position = new Vector3 ((delta.x - 400f) / 80f, pos.y, pos.z);
         }

         yield return null;
      }
   }
}


