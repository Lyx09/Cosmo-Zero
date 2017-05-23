using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ActivateOnAwake_m : NetworkBehaviour
{
    public GameObject cam;

    void Start()
    {
        if (isLocalPlayer)
        {
            cam.SetActive(true);
        }
        else
        {
            cam.SetActive(false);
        }
    }
}
