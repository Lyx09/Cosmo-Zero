using UnityEngine;
using System.Collections;

public class EscMenu : MonoBehaviour
{

    public GameObject Canvas;

    void Start()
    {
        Canvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (Canvas.gameObject.activeSelf)
            {
                Time.timeScale = 1.0f;
                Canvas.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 0.0f;
                Canvas.gameObject.SetActive(true);
            }
        }
    }
}