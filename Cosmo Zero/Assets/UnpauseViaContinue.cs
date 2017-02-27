using UnityEngine;
using System.Collections;

public class UnpauseViaContinue : MonoBehaviour {

    public GameObject panel;
    public void Unpause ()
    {
        Time.timeScale = 1.0f;
        panel.gameObject.SetActive(false);
    }
}
