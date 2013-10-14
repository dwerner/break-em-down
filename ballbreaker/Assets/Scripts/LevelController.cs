using UnityEngine;
using System.Collections;

public class LevelControler : MonoBehaviour {

   public event BallOutOfBoundsEventHandler BallOutOfBounds;
   public delegate void BallOutOfBoundsEventHandler(Object sender, System.EventArgs e);

	// Use this for initialization
	void Start () {
	
	}
	
   protected void RaiseBallOutOfBounds (System.EventArgs e) {
      BallOutOfBoundsEventHandler handler = BallOutOfBounds;
      if (handler != null) {
         handler(this, e);

      }
   }

}
