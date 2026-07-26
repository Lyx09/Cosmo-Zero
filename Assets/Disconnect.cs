using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Disconnect : MonoBehaviour
{
    public void DisconnectFunc()
    {
        // Legacy multiplayer disconnect removed (Network / MasterServer no longer exist in Unity).
        // Single-player: nothing to disconnect. Repurpose this (e.g. load a menu scene or quit) if needed.
        // Example: SceneManager.LoadScene("MainMenu");
    }
}
