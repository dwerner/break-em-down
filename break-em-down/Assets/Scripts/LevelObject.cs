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
public abstract class LevelObject : MonoBehaviour {


  private LevelController _levelController;

  public LevelController levelController {
    get {
      if (_levelController == null) {
        _levelController = FindObjectOfType<LevelController>();
      }
      return _levelController;
    }
  }

  void Awake() {
    if (this.levelController == null) {
      Debug.Log("Could not find levelController from " + this.GetType().Name);
    }
  }
}


