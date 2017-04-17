using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tuto : MonoBehaviour
{
    private int step;
    public GameObject Dialogue;
    public Text message;
    public Text quest;
    static private float BeginningZ, BeginningQ, BeginningS, BeginningD, BeginningSpace, BeginningCtrl, BeginningA, BeginningE;
    static private bool bz, bq, bs, bd, btab, bspace, bctrl, ba, be;

    void Start()
    {
        Dialogue.gameObject.SetActive(true);
        step = 0;
        BeginningZ = 0;
        BeginningQ = 0;
        BeginningS = 0;
        BeginningD = 0;
        BeginningSpace = 0;
        BeginningCtrl = 0;
        bq = false;
        bs = false;
        bd = false;
        btab = false;
        bspace = false;
        bctrl = false;
        quest.text = "";
    }

    void Update()
    {
        switch (step)
        {
            case 0:
                message.text = "Hello, \nWelcome to the Cosmo Zero Tutorial!\n";
                if (Input.anyKeyDown)
                    step = -1;
                break;
            case 1:
                message.text = "You're going to learn the basics of the game";
                if (Input.anyKeyDown)
                    step = -1;
                break;
            case -1:
                message.text = "The first thing you should now is that there's a menu to help you through this tutoriel.\nYou can access it by pressing 'Tab'";
                step = -2;
                break;
            case -2:
                quest.text = "Current quest:\n-Open the Help menu by pressing 'Tab' <color=green>(Done)</color>";
                if (!btab)
                {
                    if (Input.GetKeyDown(KeyCode.Tab))
                    {
                        Dialogue.gameObject.SetActive(false);
                        btab = true;
                    }
                    else if (Input.anyKeyDown)
                        Dialogue.gameObject.SetActive(false);
                }
                else
                {
                    if (Input.GetKeyUp(KeyCode.Tab))
                        step = 2;
                }
                break;
            case 2:
                Dialogue.gameObject.SetActive(true);
                message.text = "Good!\nNow try to move forward.\nIn order to do so, press 'Z'";
                step = 3;
                break;
            case 3:
                quest.text = "Current quest:\n-Move forward by bressing 'Z' <color=red>(To do)</color>";
                if (!bz)
                    bz = KeyPressed(ref BeginningZ, KeyCode.Z);
                else
                    step = 4;
                break;
            case 4:
                Dialogue.gameObject.SetActive(true);
                quest.text = "";
                message.text = "Well played";
                if (Input.anyKeyDown)
                    step = 5;
                break;
            case 5:
                message.text = "Now, try moving backward, to your left and to your right,\nby using the 'S','Q' and 'D' keys";
                step = 6;
                break;
            case 6:
                quest.text = "Current quest:\n-Move backward by pressing 'S' " + Done(bs)
                    + "\n-Move to your left by pressing 'Q' " + Done(bq)
                    + "\n-Move to your right by pressing 'D' " + Done(bd);
                if (bq && bs && bd)
                    step = 7;
                else
                {
                    if (!bq)
                        bq = KeyPressed(ref BeginningQ, KeyCode.Q);
                    if (!bs)
                        bs = KeyPressed(ref BeginningS, KeyCode.S);
                    if (!bd)
                        bd = KeyPressed(ref BeginningD, KeyCode.D);
                }
                break;
            case 7:
                Dialogue.gameObject.SetActive(true);
                message.text = "Nice!";
                if (Input.anyKeyDown)
                    step = 8;
                break;
            case 8:
                message.text = "You can also move up by pressing 'Space', and down by pressing 'Ctrl'\nTry it now!";
                step = 9;
                break;
            case 9:
                quest.text = "Current quest:\n-Move up by pressing 'Space' " + Done(bspace)
                    + "\n-Move down by pressing 'Ctrl' " + Done(bctrl);
                if (bspace && bctrl)
                    step = 10;
                if (!bspace)
                    bspace = KeyPressed(ref BeginningSpace, KeyCode.Space);
                if (!bctrl)
                    bctrl = KeyPressed(ref BeginningCtrl, KeyCode.LeftControl);
                break;
            case 10:
                Dialogue.gameObject.SetActive(true);
                message.text = "Great!\nYou only have one last move to learn, and it's the coolest: Rolling!";
                if (Input.anyKeyDown)
                    step = 11;
                break;
            case 11:
                message.text = "You can either roll to your left by pressing 'A', or roll to your right by pressing 'E'\nTry both now!";
                if (ba && be)
                    step = 12;
                if (!ba)
                    ba = KeyPressed(ref BeginningA, KeyCode.A);
                if (!be)
                    be = KeyPressed(ref BeginningE, KeyCode.E);
                break;
            case 12:
                Dialogue.gameObject.SetActive(true);
                message.text = "Excellent.\nNow you know all about piloting your spaceship.\nIt's time to get to the serious stuff";
                break;
            default:
                break;
        }
    }
    public string Done(bool b)
    {
        if (b)
            return "<color=green>(Done)</color>";
        else
            return "<color=red>(To do)</color>";
    }
    public bool KeyPressed(ref float Beginning, KeyCode key)
    {
        if (Beginning == 0)
        {
            if (Input.GetKeyDown(key))
            {
                Dialogue.gameObject.SetActive(false);
                Beginning = Time.time;
            }
            else if (Input.anyKeyDown)
                Dialogue.gameObject.SetActive(false);
            return false;
        }
        else
        {
            if (Input.GetKeyUp(key))
            {
                Beginning = 0;
                return false;
            }
            else if (Time.time - Beginning >= 1.10)
                return true;
            return false;
        }
    }
}