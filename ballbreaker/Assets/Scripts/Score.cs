using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

   public int score = 0;
   public LevelController levelController;

   void Start() {
      this.score = 0;
      if (this.levelController) {
         this.levelController.BrickDestroyed += (object sender) => this.increaseBy(10);
      }
   }

   void Update() {
      this.updateGUI();
   }

   void updateGUI() {
      this.GetComponent<TextMesh>().text = "Score: " + score;
   }

   public void increaseBy(int amount) {
      this.score += amount;
   }
}
