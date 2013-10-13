using UnityEngine;
using System.Collections;

public class Balls : MonoBehaviour {

   public int defaultLives = 3;
   public int curLives;

   // Use this for initialization
   void Start() {
      curLives = this.defaultLives;
   }

   // Update is called once per frame
   void Update() {

      if (this.curLives == 0) {
         Application.LoadLevel("MainMenu");
      }

      this.updateGUI();
   }

   void updateGUI() {
      this.guiText.text = "Balls: " + this.curLives.ToString();
   }

   public void decrease(int amount) {
      this.curLives -= amount;
   }
}
