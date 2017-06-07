using UnityEngine;
using System.Collections;

public class UnpauseViaContinue : MonoBehaviour {

    public GameObject panel;
    public GameObject player;
    public void Unpause ()
    {
        Time.timeScale = 1.0f;
        panel.gameObject.SetActive(false);
        player.GetComponent<SpaceshipControls>().blockMovement = false;
        player.GetComponent<SpaceshipControls>().blockRotation = false;
    }
}
