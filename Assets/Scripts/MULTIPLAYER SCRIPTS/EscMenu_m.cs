using UnityEngine;
using System.Collections;

public class EscMenu_m : MonoBehaviour
{

    public GameObject EscapePanel;
    public SpaceshipControls_m Controls;
    private bool isEscape = false;

    void Start()
    {
        isEscape = false;
    }

    void Update()
    {

        if (Input.GetKeyDown("escape"))
        {
            isEscape = !isEscape;
            EscapePanel.SetActive(isEscape);
            Controls.isInMenu = isEscape;

        }
    }
}