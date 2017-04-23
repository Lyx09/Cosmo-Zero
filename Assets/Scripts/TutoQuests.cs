using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutoQuests : MonoBehaviour
{
    private int step;
    private static int spheresdone;
    private static bool collected;
    public GameObject Dialogue, Cut, Ship, TargetedSphere;
    public Text message;
    public Text quest;
    static private float BeginningZ, BeginningQ, BeginningS, BeginningD, BeginningSpace, BeginningCtrl, BeginningA, BeginningE, StartPause;
    static private bool bz, bq, bs, bd, btab, bspace, bctrl, ba, be, btop, bbottom, bleft, bright;
    static private double lpart, rpart, tpart, bpart;
    public GameObject Sphere1, Sphere2, Sphere3, Sphere4, Sphere5, Targets;
    private Rigidbody Player;

    void Start()
    {
        Dialogue.gameObject.SetActive(true);
        step = 22;
        BeginningZ = 0;
        BeginningQ = 0;
        BeginningS = 0;
        BeginningD = 0;
        BeginningSpace = 0;
        BeginningCtrl = 0;
        BeginningA = 0;
        BeginningE = 0;
        bq = false;
        bs = false;
        bd = false;
        btab = false;
        bspace = false;
        bctrl = false;
        ba = false;
        be = false;
        btop = false;
        bbottom = false;
        bleft = false;
        bright = false;
        collected = false;
        spheresdone = 0;
        Player = GetComponent<Rigidbody>();
        quest.text = "";
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
                    step = -1;
                break;
            case -1:
                message.text = "The first thing you should now is that there's a menu to help you through this tutorial.\nYou can access it by pressing 'Tab'";
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
                quest.text = "Current quest:\n-Roll left by pressing 'A'" + Done(ba)
                    + "\n-Roll right by pressing 'E'" + Done(be);
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
                message.text = "Excellent.\nNow you can move in the direction you want.\nBut you may have also noticed that you can rotate your ship by moving your mouse around.";
                if (Input.anyKeyDown)
                    step = 13;
                break;
            case 13:
                quest.text = "Current quest:\n-Put your cursor on the left part of your screen" + Done(bleft)
                    + "\n-Put your cursor on the right part of your screen" + Done(bright)
                    + "\n-Put your cursor on the top part of your screen" + Done(btop)
                    + "\n-Put your cursor on the bottom part of your screen" + Done(bbottom);
                message.text = "Put your cursor in the borders of the screen and see what happens!";
                if (bleft && bright && btop && bbottom)
                    step = 14;
                if (Input.anyKeyDown)
                    Dialogue.gameObject.SetActive(false);
                lpart = Screen.width * 0.2;
                bleft = bleft || (Input.mousePosition[0] <= lpart);
                rpart = Screen.width * 0.8;
                bright = bright || (Input.mousePosition[0] >= rpart);
                tpart = Screen.height * 0.8;
                btop = btop || (Input.mousePosition[1] >= tpart);
                bpart = Screen.height * 0.2;
                bbottom = bbottom || (Input.mousePosition[1] <= bpart);
                break;
            case 14:
                Dialogue.gameObject.SetActive(true);
                quest.text = "";
                message.text = "Good!\n Now we can put in application what you just learned";
                if (Input.anyKeyDown)
                {
                    Dialogue.gameObject.SetActive(false);
                    StartPause = Time.time;
                    Cut.gameObject.SetActive(true);
                    step = 15;
                }
                break;
            case 15:
                if (Time.time - StartPause >= 0.6)
                {
                    Ship.transform.position = new Vector3(0, 0, 0);
                    Ship.transform.rotation = new Quaternion(0, 0, 0, 0);
                    step = 16;
                }
                break;
            case 16:
                Cut.gameObject.SetActive(false);
                Sphere1.gameObject.SetActive(true);
                Player.GetComponent<Lock>().target = Sphere1.transform;
                TargetedSphere = Sphere1;
                Dialogue.gameObject.SetActive(true);
                step = 17;
                break;
            case 17:
                message.text = "You are going to be targetting a blue sphere. Collect it!";
                quest.text = "Current quest:\n-Collect the blue sphere";
                if (Input.anyKeyDown)
                    Dialogue.gameObject.SetActive(false);
                if (collected)
                {
                    Dialogue.gameObject.SetActive(true);
                    step = 18;
                    Sphere2.gameObject.SetActive(true);
                    TargetedSphere = Sphere2;
                    collected = false;
                    Player.GetComponent<Lock>().target = Sphere2.transform;
                }
                break;
            case 18:
                message.text = "Good! Now do that 4 more times";
                quest.text = "Current quest:\n-Collect four blue spheres (" + (spheresdone).ToString() + "/4)";
                if (Input.anyKeyDown)
                    Dialogue.gameObject.SetActive(false);
                if (collected)
                {
                    spheresdone++;
                    step = 19;
                    Sphere3.gameObject.SetActive(true);
                    Player.GetComponent<Lock>().target = Sphere3.transform;
                    collected = false;
                    TargetedSphere = Sphere3;
                }
                break;
            case 19:
                quest.text = "Current quest:\n-Collect four blue spheres (" + (spheresdone).ToString() + "/4)";
                if (Input.anyKeyDown)
                    Dialogue.gameObject.SetActive(false);
                if (collected)
                {
                    spheresdone++;
                    step = 20;
                    Sphere4.gameObject.SetActive(true);
                    Player.GetComponent<Lock>().target = Sphere4.transform;
                    collected = false;
                    TargetedSphere = Sphere4;
                }
                break;
            case 20:
                quest.text = "Current quest:\n-Collect four blue spheres (" + (spheresdone).ToString() + "/4)";
                if (Input.anyKeyDown)
                    Dialogue.gameObject.SetActive(false);
                if (collected)
                {
                    spheresdone++;
                    step = 21;
                    Sphere5.gameObject.SetActive(true);
                    Player.GetComponent<Lock>().target = Sphere5.transform;
                    collected = false;
                    TargetedSphere = Sphere5;
                }
                break;
            case 21:
                quest.text = "Current quest:\n-Collect four blue spheres (" + (spheresdone).ToString() + "/4)";
                if (Input.anyKeyDown)
                    Dialogue.gameObject.SetActive(false);
                if (collected)
                {
                    spheresdone++;
                    step = 22;
                }
                break;
            case 22:
                Dialogue.gameObject.SetActive(true);
                message.text = "Good job.\nYou'll soon be ready for the real game";
                if (Input.anyKeyDown)
                    step = 23;
                break;
            case 23:
                message.text = "The last important thing you have to learn is how to shoot";
                if (Input.anyKeyDown)
                {
                    Dialogue.gameObject.SetActive(false);
                    StartPause = Time.time;
                    Cut.gameObject.SetActive(true);
                    step = 24;
                }
                break;
            case 24:
                if (Time.time - StartPause >= 0.6)
                {
                    Ship.transform.position = new Vector3(0, 0, 0);
                    Ship.transform.rotation = new Quaternion(0, 0, 0, 0);
                    step = 25;
                }
                break;
            case 25:
                message.text = "To complete this last quest,\nyou have to shoot 5 targets, by using your left click";
                quest.text = "Current quest:\n-Shoot 5 targets (" + Player.GetComponent<State>().xp.ToString() + "/5)";
                Cut.gameObject.SetActive(false);
                Targets.gameObject.SetActive(true);
                Dialogue.gameObject.SetActive(true);
                step = 26;
                break;
            case 26:
                quest.text = "Current quest:\n-Shoot 5 targets (" + Player.GetComponent<State>().xp.ToString() + "/5)";
                if (Input.anyKeyDown)
                    Dialogue.SetActive(false);
                if (Player.GetComponent<State>().xp == 5)
                    step = 27;
                break;
            case 27:
                quest.text = "";
                Dialogue.gameObject.SetActive(true);
                message.text = "Well, looks like you've completed the tutorial.\nWell played";
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
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == TargetedSphere)
        {
            collected = true;
            Destroy(other.gameObject);
        }
    }
}
