using UnityEngine;
using System.Collections;

public class EscMenu : MonoBehaviour
{

    public GameObject Canvas;
    public GameObject Camera;
    bool Paused = false;

    void Start()
    {
        Canvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (Paused == true)
            {
                Time.timeScale = 1.0f;
                Canvas.gameObject.SetActive(false);
                //Camera.GetComponent<AudioSource>().Play();
                Paused = false;
            }
            else
            {
                Time.timeScale = 0.0f;
                Canvas.gameObject.SetActive(true);
                //Camera.GetComponent<AudioSource>().Pause();
                Paused = true;
            }
        }
    }
    /*public void Resume()
    {
        Time.timeScale = 1.0f;
        Camera.GetComponent<AudioSource>().Play();
    }*/
}