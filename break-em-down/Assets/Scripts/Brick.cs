using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Brick : LevelObject {

   public int hitPoints =1;

   void Start(){
      if (levelController != null) {
         levelController.brickCount += 1;
      }
   }

   IEnumerator OnCollisionEnter (Collision c){

      hitPoints -= 1;

      if (hitPoints <= 0) {

         yield return StartCoroutine(this.Pulse());

         this.gameObject.SetActive(false);

         if (levelController != null) {
            levelController.RaiseBrickDestroyed();
         }
      }

   }

   IEnumerator Pulse(){
      var sizes = new List<float>() {
         1.0f,1.1f,1.2f,1.3f,1.2f,1.1f,1.0f
      };

      var initialScale = transform.localScale;

      foreach (var i in sizes){
         transform.localScale = initialScale * i;
         yield return null;
      }
      yield return null;
   }

}
