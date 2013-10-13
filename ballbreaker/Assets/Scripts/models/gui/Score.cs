using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

   public static int score = 0;

   void Start() {
      this.setScore (0);
   }

   // Update is called once per frame
   void Update () {
      this.setScore (score);
   }

   void setScore(int score) {

      this.guiText.text = "Scoreee: " + score;

   }
}
