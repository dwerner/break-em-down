using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour {

   public LevelController levelController;

   public void Start(){
      this.levelController = LevelController.getInstance();
   }

   void OnCollisionEnter (Collision c) {

	  if (c.gameObject.GetComponent<GameBall>() != null) {
         c.gameObject.SetActive(false); // no longer active if we are out of bounds
         levelController.RaiseBallOutOfBounds();
      }
   }
}
