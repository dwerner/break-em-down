using System;
using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
   public GameObject ballPrefab;

   public event BallOutOfBoundsEventHandler BallOutOfBounds;
   public delegate void BallOutOfBoundsEventHandler(object sender);

   public event BrickDestroyedEventHandler BrickDestroyed;
   public delegate void BrickDestroyedEventHandler(object sender);

   public event EventHandler GameOver;
   public event EventHandler LevelWon;

   public int brickCount;

   void Start() {
   }

   public void RaiseGameOver() {
      
      Debug.Log("-- game over --");
      Application.LoadLevel("MainMenu");

      EventHandler handler = GameOver;
      if (handler != null) {
         handler(this, new System.EventArgs());
      }

   }

   public void RaiseLevelWon() {

      Debug.Log("-- level won --");
      Application.LoadLevel("MainMenu");

      EventHandler handler = LevelWon;
      if (handler != null) {
         handler(this, new System.EventArgs());
      }

   }


   public void RaiseBallOutOfBounds() {
      BallOutOfBoundsEventHandler handler = BallOutOfBounds;
      
      Debug.Log("ball lost");

      if (handler != null) {
         handler(this);

      }

      Instantiate(this.ballPrefab);
   }

   public void RaiseBrickDestroyed() {

      this.brickCount -= 1;

      BrickDestroyedEventHandler handler = BrickDestroyed;
      if (handler != null) {
         handler(this);

      }

      if (this.brickCount == 0) {
         Debug.Log("Out of bricks...");
         this.RaiseLevelWon();
      }
   }
}
