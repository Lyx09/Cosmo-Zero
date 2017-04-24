using UnityEngine;
using System.Collections;

public class SetRadar : MonoBehaviour
{
    public GameObject radar;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetKeyDown(KeyCode.N))
        {
            radar.SetActive(true);
        }
	}
}
