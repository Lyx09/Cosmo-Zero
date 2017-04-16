using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tuto : MonoBehaviour
{
    private int step;
    public GameObject Dialogue;
    public Text message;
    private float BeginningZ, BeginningQ, BeginningS, BeginningD;
    private bool bq, bs, bd;

    void Start()
    {
        Dialogue.gameObject.SetActive(true);
        step = 0;
        BeginningZ = 0;
        BeginningQ = 0;
        BeginningS = 0;
        BeginningD = 0;
        bq = false;
        bs = false;
        bd = false;
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
                if (Input.anyKeyDown)
                    step = 2;
                break;
            case 2:
                message.text = "First of all, try to move forward.\nIn order to do so, press 'Z'";
                if (Input.anyKeyDown)
                    step = 3;
                break;
            case 3:
                Dialogue.gameObject.SetActive(false);
                if (BeginningZ == 0)
                {
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        BeginningZ = Time.time;
                    }
                }
                else
                {
                    if (Input.GetKeyUp(KeyCode.Z))
                    {
                        BeginningZ = 0;
                    }
                    else if (Time.time - BeginningZ >= 1.10)
                        step = 4;
                }
                break;
            case 4:
                Dialogue.gameObject.SetActive(true);
                message.text = "Well played";
                if (Input.anyKeyDown)
                    step = 5;
                break;
            case 5:
                message.text = "Now, try moving backwards, to your left and to your right,\nby using the 'S','Q' and 'D' keys";
                if (Input.anyKeyDown)
                    step = 6;
                break;
            case 6:
                Dialogue.gameObject.SetActive(false);
                if (bq && bs && bd)
                    step = 7;
                if (BeginningQ == 0)
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                        BeginningQ = Time.time;
                }
                else
                {
                    if (Input.GetKeyUp(KeyCode.Q))
                    {
                        BeginningQ = 0;
                    }
                    else if (Time.time - BeginningQ >= 0.75)
                        bq = true;
                }
                if (BeginningS == 0)
                {
                    if (Input.GetKeyDown(KeyCode.S))
                        BeginningS = Time.time;
                }
                else
                {
                    if (Input.GetKeyUp(KeyCode.S))
                    {
                        BeginningS = 0;
                    }
                    else if (Time.time - BeginningS >= 0.75)
                        bs = true;
                }
                if (BeginningD == 0)
                {
                    if (Input.GetKeyDown(KeyCode.D))
                        BeginningD = Time.time;
                }
                else
                {
                    if (Input.GetKeyUp(KeyCode.D))
                    {
                        BeginningD = 0;
                    }
                    else if (Time.time - BeginningD >= 0.75)
                        bd = true;
                }
                break;
            case 7:
                Dialogue.gameObject.SetActive(true);
                message.text = "Nice!";
                if (Input.anyKeyDown)
                    step = 8;
                break;
            case 8:
                Dialogue.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}