using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController {

   #region Singleton_impl


   // maybe a singleton isn't the right way... perhaps just use a levelController to control the behavior of the level
   static private GameController _instance;

   static public GameController getInstance() {
      if (_instance == null) {
         _instance = new GameController();
      }
      return _instance;
   }

   private GameController() {}

   #endregion


}

