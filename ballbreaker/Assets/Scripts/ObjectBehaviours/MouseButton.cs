using UnityEngine;
using System.Collections;

public class MouseButton : MonoBehaviour {

   void OnMouseDown() {
      Debug.Log("Mousedown on: " + this.name);
   }

   void OnMouseUp() {
      Debug.Log("Mouseup on: " + this.name);
   }

}
