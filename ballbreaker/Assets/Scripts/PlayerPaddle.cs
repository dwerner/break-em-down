using UnityEngine;
using System.Collections;

public class PlayerPaddle : MonoBehaviour {
	
	public float speed = 5;
	
	public static float zPos = 0;
	public float yPos = 0;
	public float xPos = 0;
	
	public float xBoundLeft = 0; // i edited the properties in the inspector but they did not carry over here
	public float xBoundRight = 0; // when do they get set?
	public float yBoundTop = 0; // 
	
	private Transform _t;
	
	void Awake() {
		_t = transform;
	}
	
	// Use this for initialization
	void Start () {
		_t.position = new Vector3(0, this.yPos, this.xPos);
	}
	
	// Update is called once per frame
	void Update () {
		
		var x = _t.position.x;
		var h = Input.GetAxis("Horizontal");
		
		if (h != 0) {
			
			// noticed that x could go beyond boundary, would need to put Max (float, float) or Min (float, float)
			if (x >= xBoundLeft && x <= xBoundRight) {
				_t.position = new Vector3(x + h * speed * Time.deltaTime, this.yPos, zPos);
			}
			else if (x < xBoundLeft) {
				// after implementing this we see jittering... will need to reapproach how we deal with collision detection
				// probably the built in way Dan mentioned
				_t.position = new Vector3(this.xBoundLeft, this.yPos, zPos);
			}
			else if (x > xBoundRight) {
				_t.position = new Vector3(this.xBoundRight, this.yPos, zPos);
			}
		}
		
	}
}
