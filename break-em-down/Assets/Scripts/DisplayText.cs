// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.Collections;

public class DisplayText : LevelObject {

  private TextMesh textMesh;

  private float delay = 0.5f;

  private string textToShow;

  IEnumerator Start(){

    textMesh = GetComponent<TextMesh>();

    levelController.BallOutOfBounds += (object sender) => {
      if (Balls.balls >= 1) {
        delay = 2;
        textToShow = "Ball Lost";
      }
    };

    levelController.LevelStart += (obj, arg) => {
      delay = 3f;
      textToShow = levelController.currentLevel;
    };


    levelController.GameOver += new EventHandler((obj, arg) => {
      delay = 5;
      textToShow = "Game Over... :("+ Environment.NewLine +
                   "Score: " + Score.score; 
    });

    levelController.LevelWon += new EventHandler((obj, arg) => {
      delay = 3;
      textToShow = "Level Complete!";
    });

    levelController.BrickDestroyed += (o)=>{
      delay = 0.4f;
      int amt = 10;
      if (textToShow.StartsWith("+")){
        amt += int.Parse(textToShow);
      }
      textToShow = "+"+amt;
    };
    
    while(true){
      if (textToShow != String.Empty){
        var temp = textToShow;
        textToShow = "";
        yield return StartCoroutine(this.showText(temp, delay));
      }
      yield return null;
    }
  }

  IEnumerator showText(string text, float seconds){
    textMesh.text = text;
    Debug.Log("Detected Text being displayed: " + text);
    yield return StartCoroutine(this.hideText(seconds));
  }

  IEnumerator hideText(float seconds) {
    yield return new WaitForSeconds(seconds);
    textMesh.text = "";
  }

}


