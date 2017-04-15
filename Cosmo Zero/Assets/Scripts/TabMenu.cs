using UnityEngine;
using System.Collections;

public class TabMenu : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject Display;
    void Start ()
    {
        Canvas.gameObject.SetActive(false);
	}
	
	void Update ()
    {
	    if (Canvas.gameObject.activeSelf)
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                Canvas.gameObject.SetActive(false);
                Display.gameObject.SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Canvas.gameObject.SetActive(true);
                Display.gameObject.SetActive(false);
            }
        }
	}
}
