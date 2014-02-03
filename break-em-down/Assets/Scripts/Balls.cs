using UnityEngine;
using System.Collections;

public class Balls : LevelObject {

   public int defaultLives = 3;
   public int curLives;



   // Use this for initialization
   void Start() {
      curLives = this.defaultLives;
      if (this.levelController) {
         this.levelController.BallOutOfBounds += (object sender) => this.decreaseBy(1);
      }
   }

   // Update is called once per frame
   void Update() {

      if (this.curLives == 0) {
         Application.LoadLevel("MainMenu");
      }

      this.updateGUI();
   }

   void updateGUI() {
      this.GetComponent<TextMesh>().text = "Balls: " + this.curLives.ToString();
   }

   public void decreaseBy(int amount) {
      this.curLives -= amount;
      if (this.curLives < 0) {
        levelController.RaiseGameOver();
      }
   }
}
