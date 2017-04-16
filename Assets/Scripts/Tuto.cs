using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tuto : MonoBehaviour
{
    private int step;
    public GameObject Dialogue;
    public Text message;

    void Start()
    {
        Dialogue.gameObject.SetActive(true);
        step = 0;
    }

    void Update()
    {
        switch (step)
        {
            case 0:
                message.text = "Hello, \nWelcome to the Cosmo Zero Tutorial!\n";
                if (Input.anyKeyDown)
                    step = 1;
                break;
            case 1:
                message.text = "You're going to learn the basics of the game";
                break;
            default:
                break;
        }
    }
}

