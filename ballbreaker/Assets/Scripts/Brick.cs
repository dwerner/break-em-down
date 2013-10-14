using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Brick : MonoBehaviour {

	public int hitPoints =1;

	IEnumerator OnCollisionEnter (Collision c){

		hitPoints -= 1;

		var sizes = new List<float>() {
			1.0f,1.1f,1.2f,1.3f,1.2f,1.1f,1.0f
		};

		var initialScale = transform.localScale;

		foreach (var i in sizes){
			transform.localScale = initialScale * i;
			yield return 0;
		}
	
		if (hitPoints <= 0) {
			this.gameObject.active = false;

		}
		yield return 0;
	}
}
