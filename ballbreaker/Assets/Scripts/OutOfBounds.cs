using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour {

   public LevelController levelController;

   void OnCollisionEnter (Collision c) {
      c.gameObject.SetActive(false); // no longer active if we are out of bounds

      levelController.RaiseBallOutOfBounds();
   }
}
