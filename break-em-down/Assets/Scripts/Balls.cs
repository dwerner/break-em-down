using UnityEngine;
using System.Collections;

public class Balls : LevelObject {

   public static int balls = 5;

   public static void Reset(){
      balls = 5;
   }

   // Use this for initialization
   void Start() {
      if (this.levelController) {
         this.levelController.BallOutOfBounds += (object sender) => this.decreaseBy(1);

      }
   }

   // Update is called once per frame
   void Update() {
    this.GetComponent<TextMesh>().text = "Balls: " + balls.ToString();
  }

   public void decreaseBy(int amount) {
      balls -= amount;
      if (balls < 1) {
        levelController.RaiseGameOver();
      }
   }
}
