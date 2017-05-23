using UnityEngine;
using System.Collections;

public class EscMenu : MonoBehaviour
{

    public GameObject EscapePanel;

    void Start()
    {
        EscapePanel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (EscapePanel.gameObject.activeSelf)
            {
                Time.timeScale = 1.0f;
                EscapePanel.gameObject.SetActive(false);
            }
            else
            {
                Time.timeScale = 0.0f;
                EscapePanel.gameObject.SetActive(true);
            }
        }
    }
}