using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour {

   public Balls balls;

   void OnCollisionEnter (Collision c) {

      if (balls != null) balls.decrease(1);


   }
}
