using UnityEngine;
using System.Collections;

public class Score : LevelObject {

   public static int score = 0;

   void Start() {

      if (this.levelController) {
         this.levelController.BrickDestroyed += (object sender) => this.increaseBy(10);

         //only add the score to the total running if the level has been won
         //this.levelController.LevelWon += (object sender, System.EventArgs e) => ;
         this.levelController.LevelWon += (object sender, System.EventArgs e) => Debug.Log("TODO: send high score to server");
      }
   }

   void Update() {
      this.updateGUI();
   }

   void updateGUI() {
      this.GetComponent<TextMesh>().text = "Score: " + score;
   }

   public void increaseBy(int amount) {
      score += amount;
   }
}
