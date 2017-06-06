using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CountCPs : MonoBehaviour {
    public GameObject panel;
    private int count;
    private bool CP1;
    private bool CP2;
    private bool CP3;
    private bool CP4;
    private bool CP;


    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (CP1 && CP2 && CP3 && CP4 && CP)
        {
            panel.SetActive(true);
            //SceneManager.LoadScene();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CP"))
        {
            CP = true;
        }
        if (other.CompareTag("CP2"))
        {
            CP2 = true;
        }
        if (other.CompareTag("CP3"))
        {
            CP3 = true;
        }
        if (other.CompareTag("CP4"))
        {
            CP4 = true;
        }
        if (other.CompareTag("CP1"))
        {
            CP1 = true;
        }
    }
}
