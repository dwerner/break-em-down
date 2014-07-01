using UnityEngine;
using System.Collections;
public class NewGameScript : MonoBehaviour {
  
  public string levelName;

  void OnMouseUpAsButton() {
    if (levelName.Trim() != string.Empty) {
      Balls.Reset();
      Score.Reset();
      Application.LoadLevel(this.levelName);

    }
  }
}
