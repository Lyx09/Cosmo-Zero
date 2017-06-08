using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CountCPs : MonoBehaviour {
    public GameObject panel;
    private int count;
    public bool CP1;
    public bool CP2;
    public bool CP3;
    public bool CP4;
    public bool CP;
    public GameObject chrono;


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
            chrono.SetActive(false);
            GetComponent<FullSolo>().raced = true;
            Debug.Log(3);
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
