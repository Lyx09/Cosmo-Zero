using UnityEngine;
using System.Collections;

public class PlayButton : MonoBehaviour {

	private bool _run = false;
	private PlayerControl _playerControl;
	
	void Start(){
		_playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}
	
	void OnGUI () {
	    if (_run == false && GUI.Button (new Rect (Screen.width/2-125,Screen.height/2-35,250,70), "Play")) {
			_playerControl.status = true;
	        _run = true;
	    }else if (_run == true && GUI.Button (new Rect (10,10,100,50), "Stop")) {
			_playerControl.status = false;
	        _run = false;
	    }
		
		GUI.Label (new Rect(10, 100, 250, 25), "CONTROLS:");
		GUI.Label (new Rect(10, 125, 250, 25), "W - Boost");
		GUI.Label (new Rect(10, 150, 250, 25), "S - Air Break");
		GUI.Label (new Rect(10, 175, 250, 25), "A - Rotate left");
		GUI.Label (new Rect(10, 200, 250, 25), "D - Rotate right");				
		GUI.Label (new Rect(10, 225, 250, 25), "Mouse look to rotate camera");

	}
	
}
