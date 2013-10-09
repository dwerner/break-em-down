using System;
using UnityEngine;

namespace AssemblyCSharp {
	public class QuitScript {
		
		public void OnMouseUpAsButton(){
			Handheld.Vibrate();
			Application.Quit();
		}
		
		public QuitScript () {
			
		}
	}
}

