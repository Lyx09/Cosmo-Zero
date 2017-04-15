using UnityEngine;
using System.Collections;

public class TutoMenu : MonoBehaviour
{
<<<<<<< HEAD
    public GameObject Panel;
    public GameObject Disp;
    // Use this for initialization
    void Start()
    {
        Panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Panel.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Panel.gameObject.SetActive(true);
                Disp.gameObject.SetActive(false);
=======
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
>>>>>>> origin/master
            }
        }
        else
        {
<<<<<<< HEAD
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                Panel.gameObject.SetActive(false);
                Disp.gameObject.SetActive(true);
=======
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Canvas.gameObject.SetActive(true);
                Display.gameObject.SetActive(false);
>>>>>>> origin/master
            }
        }
    }
}