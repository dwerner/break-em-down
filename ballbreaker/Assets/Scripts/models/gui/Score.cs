using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

   public int score = 0;

   void Start() {
      this.score = 0;
   }

   void Update() {
      this.updateGUI();
   }

   void updateGUI() {
      this.guiText.text = "Score: " + score;
   }

   public void increaseBy(int amount) {
      this.score += amount;
   }
}
