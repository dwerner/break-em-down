using System;
using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

   public event BallOutOfBoundsEventHandler BallOutOfBounds;
   public delegate void BallOutOfBoundsEventHandler(object sender);

   public event BrickDestroyedEventHandler BrickDestroyed;
   public delegate void BrickDestroyedEventHandler(object sender);

   public event EventHandler GameOver;

	void Start () {
	
	}
	
   public void RaiseGameOver () {
      
      Debug.Log("-- game over --");
      Application.LoadLevel("MainMenu");

      EventHandler handler = GameOver;
      if (handler != null) {
         handler(this, new System.EventArgs());
      }

   }

   public void RaiseBallOutOfBounds () {
      BallOutOfBoundsEventHandler handler = BallOutOfBounds;
      
      Debug.Log("ball lost");

      if (handler != null) {
         handler(this);

      }

      var ball = Resources.Load("Prefabs/Ball");
      Instantiate(ball);
   }

   public void RaiseBrickDestroyed(){

      BrickDestroyedEventHandler handler = BrickDestroyed;
      if (handler != null) {
         handler(this);

      }
   }

}
