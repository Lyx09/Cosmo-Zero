using UnityEngine;
using System.Collections;

public class SetEnemies : MonoBehaviour {
    public GameObject enemies;
    public GameObject camera;
    public GameObject disp;
    public GameObject uneautrevariable;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.B))
        {
            enemies.SetActive(true);
            camera.SetActive(false);
            disp.SetActive(false);
            uneautrevariable.SetActive(false);
        }
	}
}
