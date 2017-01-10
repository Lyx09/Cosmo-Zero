using UnityEngine;
using System.Collections;

/// <summary>
/// Componente para todo lo referente a la camara 
/// </summary>
public class CameraScript : MonoBehaviour {
	
	public Ray ray;		
	public Vector2 mousePos;
	
	private Transform _target;
	private GameObject _player;
	private Vector3 _wantedPosition;
	
	public float distance = 50.0f;
	public float height = 3.0f;
	public float damping = 15.0f;
	public float rotationDamping = 1.0f;
	
	void Start(){
		_player = GameObject.FindGameObjectWithTag("Player");
		_target=_player.transform;
	}
	
	void Update () {
		ray = Camera.main.ScreenPointToRay(mousePos);
	}
	
	void LateUpdate(){
		SmoothFollow();
	}
	
	void SmoothFollow(){
		//Follow player smoothly
		_wantedPosition = _target.TransformPoint(0, height, -distance);
		transform.position = Vector3.Lerp (transform.position, _wantedPosition, Time.deltaTime * damping);
		
		Quaternion _wantedRotation = Quaternion.LookRotation(_target.position - transform.position, _target.up);
		
		transform.rotation = Quaternion.Slerp (transform.rotation, _wantedRotation, Time.deltaTime * rotationDamping);

		transform.LookAt (_target, _target.up);


		

	}
	
}
