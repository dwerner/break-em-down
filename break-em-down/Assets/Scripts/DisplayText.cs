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
using System.Collections.Generic;
public class DisplayText : LevelObject {

  private TextMesh textMesh;
  private Vector3 initialPosition;
  private float delay = 0.5f;
  int amt = 0;
  private string textToShow;

  IEnumerator Start() {

    initialPosition = this.transform.position;

    textMesh = GetComponent<TextMesh>();

    levelController.BallOutOfBounds += (object sender) => {
      if (Balls.balls >= 1) {
        delay = 2;
        textToShow = "Ball Lost =|";
      }
    };

    levelController.LevelStart += (obj, arg) => {
      delay = 3f;
      textToShow = levelController.currentLevel;
    };


    levelController.GameOver += new EventHandler((obj, arg) => {
      delay = 5;
      textToShow = @"Game Over (T.T)" + Environment.NewLine +
        "Total Score: " + Score.score; 
    });

    levelController.LevelWon += new EventHandler((obj, arg) => {
      delay = 3;
      textToShow = "Level Complete! XD";
    });

    //TODO: int comboMultiplier = 1;

    levelController.BrickDestroyed += (o) => {

      delay = 0.4f;

      var combo = amt > 0;

      amt += 10;

      int comboAmount = 0;
      if (combo) {
        comboAmount = (int)(amt * 1.5f);
      } 

      textToShow = "+" + (amt + comboAmount) + (combo ? " COMBO" : "");

      Score.increaseBy(amt);

    };
    
    while (true) {
      if (textToShow != String.Empty) {
        var temp = textToShow;
        textToShow = "";
        yield return StartCoroutine(this.showText(temp, delay));
      }
      yield return null;
    }
  }

  IEnumerator showText(string text, float seconds) {
    textMesh.text = text;
    Debug.Log("Detected Text being displayed: " + text);

    yield return StartCoroutine(this.hideText(seconds));

  }

  IEnumerator hideText(float seconds) {
    var startTime = DateTime.UtcNow;
    yield return StartCoroutine(this.Pulse());
    var delay = seconds - (DateTime.UtcNow - startTime).Seconds; 
    yield return new WaitForSeconds(delay);
    textMesh.text = "";
    amt = 0;
  }

}


