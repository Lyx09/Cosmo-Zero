using UnityEngine;
using System.Collections;

public class SpeedParticles : MonoBehaviour {
	
	private PlayerControl _player;
	private float speed;
	
	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
						
	}
	
	// Update is called once per frame
	void Update () {
		if (_player.status){
			if(_player.currrentSpeed <= 15)
				GetComponent<ParticleEmitter>().emit = false;
			else{
				GetComponent<ParticleEmitter>().emit = true;
				Vector3 aux = GetComponent<ParticleEmitter>().localVelocity;
				aux.z = -(_player.currrentSpeed*50)/20;
				GetComponent<ParticleEmitter>().localVelocity = aux;
			}
		}
		else
			GetComponent<ParticleEmitter>().emit = false;
	}
}
