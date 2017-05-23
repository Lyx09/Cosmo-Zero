using UnityEngine;
using System.Collections;

public class EscMenu_m : MonoBehaviour
{

    public GameObject EscapePanel;
    public ShipCtrls_multi Controls;

    void Start()
    {
        EscapePanel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (EscapePanel.gameObject.activeSelf) //check tqhat
            {
                Controls.blockRotation = true;
                Controls.blockMovement = true;
            }
            else
            {
                Controls.blockRotation = false;
                Controls.blockMovement = false;
            }
            EscapePanel.gameObject.SetActive(!EscapePanel.gameObject.activeSelf);
        }
    }
}