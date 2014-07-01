using UnityEngine;
using System.Collections;
public class OutOfBounds : LevelObject {

  void OnCollisionEnter(Collision c) {

    if (c.gameObject.GetComponent<GameBall>() != null) {
      //c.gameObject.SetActive(false); // no longer active if we are out of bounds
      levelController.RaiseBallOutOfBounds();
    }
  }
}
