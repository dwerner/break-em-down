using UnityEngine;
using System.Collections;

public class TouchButton : MonoBehaviour {

	// Update is called once per frame
	void Update () {

      if (Input.touchCount > 0) {
         Debug.Log("Touches: " + Input.touchCount);
         print("Touches: " + Input.touchCount);
      }
	
	}
}
