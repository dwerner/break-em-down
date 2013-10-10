﻿using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class PlayerPaddle : MonoBehaviour {
	
	public float speed = 20; 

	public float leftLimit = -6f;
	public float rightLimit = 6f;
	public bool touchesExist;

	//Changing this into an IEnumerator - so re-entry happens once per frame, and Update is never needed
	IEnumerator Start () {
		while (true) {
			var h = Input.GetAxis("Horizontal");

			if (h != 0) {
				var move = h * Time.deltaTime * speed;

				/* 
				 * For the paddle, perhaps it does make more sense to control via position, and limit based on a static width.
				 * The alternative is raycasting, and predicting the movement of the object prior to it moving, and stopping it.
				 * 
				 * Otherwise we just get intersections, and they don't feel very realistic. 
				 * 
				 * *Note that changing the masses of the targets did have an effect on the kinds of intersections that were allowed before the object rebounds.
				 */
				if (Mathf.Abs (h) > 0) {
					if (!(this.rigidbody.position.x + move < this.leftLimit) && 
					    !(this.rigidbody.position.x + move > this.rightLimit)) { 
										
						this.rigidbody.position += new Vector3 (move, 0.0f, 0.0f);
					}
				}
			} else {
				this.rigidbody.velocity *= 0;
			}


			if (Input.touchCount > 1){
				var touches = (from x in Input.touches
				               where x.phase == TouchPhase.Moved
				               select x).ToArray();

				if (touches.Count() > 0) {
					touchesExist = true;
					this.rigidbody.position += new Vector3 (
						this.rigidbody.position.x + touches [0].deltaPosition.x,
						this.rigidbody.position.y + touches [0].deltaPosition.y, 0.0f);
				}
			}

			yield return null;
		}
	}

}
