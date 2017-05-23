using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EscMenu_m : NetworkBehaviour
{

    public GameObject EscapePanel;
    public ShipCtrls_multi Controls;
    private bool isEscape = false;

    void Start()
    {
        isEscape = false;
    }

    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown("escape"))
        {
            isEscape = !isEscape;
            Controls.isInMenu = isEscape;

        }
    }
}