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
      this.GetComponent<TextMesh>().text = "Balls: " + this.curLives.ToString();
   }

   public void decreaseBy(int amount) {
      this.curLives -= amount;
   }
}
