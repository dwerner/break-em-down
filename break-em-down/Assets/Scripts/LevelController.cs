using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class LevelController : MonoBehaviour {


  public delegate void BallOutOfBoundsEventHandler(object sender);
  public delegate void BeforeBallOutOfBoundsEventHandler(object sender);
  public delegate void BrickDestroyedEventHandler(object sender);
  public delegate void StartPlayHandler(object sender);

  public event BallOutOfBoundsEventHandler BallOutOfBounds;
  public event BallOutOfBoundsEventHandler BeforeBallOutOfBounds;
  public event BrickDestroyedEventHandler BrickDestroyed;
  public event EventHandler GameOver;
  public event EventHandler LevelWon;
  public event EventHandler LevelStart;
  public event StartPlayHandler StartPlay;


  public int brickCount;
  private static LevelController instance;

  public static LevelController getInstance() {
    return instance;
  }

  // when new scenes are added to the project, add them here 
  // (or be clever and load a list of them and refactor this out)
  private List<string> levels =
    new List<string>(){
      "level1", 
      "level2",
      "level3",
      "level4",
      "level5",
      "level6",
      "level7",
      "level8"
    };
  public string currentLevel;

  void Awake() {
    instance = this; // assign value to singleton upon instantiation
  }

  IEnumerator Start() {
    this.currentLevel = Application.loadedLevelName;
    Debug.Log(this.currentLevel);
    this.RaiseLevelStart();
    while (true) {
      yield return null;
    }
  }

  public void RaiseGameOver() {

    Debug.Log("-- game over --");

    EventHandler handler = GameOver;
    if (handler != null) {
      handler(this, new System.EventArgs());
    }

    this.StartCoroutine(this.LoadMainMenu());
   
  }

  IEnumerator LoadMainMenu() {
    yield return new WaitForSeconds(5);
    Application.LoadLevel("MainMenu");
  }

  private string getNextLevel() {
    int idx = levels.IndexOf(this.currentLevel);
    if (idx + 1 == levels.Count) {
      return "MainMenu";
    } else {
      return levels [idx + 1];
    }
  }

  public void RaiseLevelWon() {

    Debug.Log("-- level won --");

    EventHandler handler = LevelWon;
    if (handler != null) {
      handler(this, new System.EventArgs());
    }

    this.StartCoroutine(this.loadNextLevel());
  }

  IEnumerator loadNextLevel() {
    yield return new WaitForSeconds(3);
    Application.LoadLevel(this.getNextLevel());
  }

  public void RaiseLevelStart() {
    Debug.Log("Level Start");
    
    EventHandler handler = LevelStart;
    if (handler != null) {
      handler(this, new System.EventArgs());
    }
  }

  public void Pause() {
    Time.timeScale = 0;
  }

  public bool isPaused { get { return Time.timeScale == 0; } }

  public void Resume() {
    Time.timeScale = 1;
  }

  public void RaiseBallOutOfBounds() {

    BallOutOfBoundsEventHandler beforeHandler = BeforeBallOutOfBounds;

    if (beforeHandler != null) {
      Debug.Log("BeforeRaiseBallOutOfBounds");
      beforeHandler(this);

    }

    BallOutOfBoundsEventHandler handler = BallOutOfBounds;

    if (handler != null) {
      Debug.Log("RaiseBallOutOfBounds");
      handler(this);
    }
  }

  public void RaiseStartPlay() {
    StartPlayHandler handler = StartPlay;
    Debug.Log("RaiseStartPlay");
    if (handler != null) {
      handler(this);
    }
  }

  public void RaiseBrickDestroyed() {

    Debug.Log("RaiseBrickDestroyed");
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
