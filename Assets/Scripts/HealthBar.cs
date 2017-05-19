using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Image image;
	// Use this for initialization
	void Start ()
    {
        image = GetComponent<Image>();
        Color c = image.color;
        c.a = 0.5F;
        image.color = c;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
