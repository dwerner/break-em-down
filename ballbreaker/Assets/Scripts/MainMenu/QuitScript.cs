using System;
using UnityEngine;

public class QuitScript : MonoBehaviour {
	
	public void OnMouseUpAsButton(){
		Handheld.Vibrate();
		Application.Quit();
	}
	
	public QuitScript () {
		
	}
}


