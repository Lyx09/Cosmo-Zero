using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Disconnect : MonoBehaviour
{
    public void DisconnectFunc()
    {
        Network.Disconnect();
        MasterServer.UnregisterHost();
    }
}
