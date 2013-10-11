using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour {
	void OnCollisionEnter (Collision c) {
		Application.LoadLevel ("MainMenu");
	}
}
