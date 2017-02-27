using UnityEngine;
using System.Collections;

public class UnpauseViaContinue : MonoBehaviour {

    public GameObject Canvas;
    public GameObject Camera;
    bool Paused = false;
    void Start () {
        Canvas.gameObject.SetActive(false);

    }

    void Update () {
        Time.timeScale = 1.0f;
        Canvas.gameObject.SetActive(false);
        Paused = false;
    }
}
