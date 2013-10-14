using UnityEngine;
using System.Collections;

public class LevelControler : MonoBehaviour {

   public event BallOutOfBoundsEventHandler BallOutOfBounds;
   public delegate void BallOutOfBoundsEventHandler();

	// Use this for initialization
	void Start () {
	
	}
	
   protected void OnBallOutOfBounds () {
      if (BallOutOfBoundsEventHandler != null) {
         BallOutOfBoundsEventHandler();
      }
   }

}
