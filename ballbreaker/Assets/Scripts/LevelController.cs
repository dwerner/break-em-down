﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelController : MonoBehaviour {
  public GameObject ballPrefab;
  public GameObject inGameMenu;

  public event BallOutOfBoundsEventHandler BallOutOfBounds;
  public delegate void BallOutOfBoundsEventHandler(object sender);

  public event BrickDestroyedEventHandler BrickDestroyed;
  public delegate void BrickDestroyedEventHandler(object sender);

  public event EventHandler GameOver;
  public event EventHandler LevelWon;

  public int brickCount;

  private static LevelController instance;
  public static LevelController getInstance(){
    return instance;
  }

  private List<string> levels =
    new List<string>(){
      "level1", 
      "level2",
      "level3",
      "level4",
      "level5",
      "level6",
      "level7"
    };

  private string currentLevel;

  IEnumerator Start() {
    LevelController.instance = this;
    this.currentLevel = Application.loadedLevelName;
    Debug.Log(this.currentLevel);

    while (true) {
      if (Input.GetKeyDown(KeyCode.Escape)){
        if (this.isPaused){
          this.inGameMenu.SetActive(false);
          this.Resume();
          Debug.Log("unpaused");
        } else {
          this.Pause();
          this.inGameMenu.SetActive(true);
          Debug.Log("pause");
        }
      }
      yield return null;
    }
  }

  public void RaiseGameOver() {

    Debug.Log("-- game over --");
    Application.LoadLevel("MainMenu");

    EventHandler handler = GameOver;
    if (handler != null) {
      handler(this, new System.EventArgs());
    }

  }

  private string getNextLevel(){
    int idx = levels.IndexOf(this.currentLevel);
    if (idx+1 == levels.Count) {
      return "MainMenu";
    }
    else {
      return levels[idx + 1];
    }
  }

  public void RaiseLevelWon() {

    Debug.Log("-- level won --");
    Application.LoadLevel(this.getNextLevel());

    EventHandler handler = LevelWon;
    if (handler != null) {
      handler(this, new System.EventArgs());
    }

  }

  public void Pause() {
    Time.timeScale = 0;
  }

  public bool isPaused { get { return Time.timeScale == 0; } }

  public void Resume () {
    Time.timeScale = 1;
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
