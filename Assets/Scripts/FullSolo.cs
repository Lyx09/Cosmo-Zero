using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FullSolo : MonoBehaviour
{
    public static int enemycount;
    private bool sounded = false;
    private int step = 0;
    public GameObject Dialogue, Ship, Health, Lock, Cross;
    public Image Cut;
    public Text speaker, message, quest, side;
    private Rigidbody Player;
    public AudioSource next, bug;
    private int buggy = 0;
    private float BeginningZ, BeginningQ, BeginningS, BeginningD;
    string cz = "red";
    string cq = "red";
    string cs = "red";
    string cd = "red";
    private float StartPause;
    private int minerai;
    public GameObject min1, min2, min3, min4;
    public GameObject Lawson;
    public GameObject HQ;
    private int initialmoney;
    public GameObject Ixal;
    public bool raced = false;
    public GameObject victorypanel;
    private bool skillquest;

    private List<string> sidequests;

    // Use this for initialization
    void Start ()
    {
        Player = GetComponent<Rigidbody>();
        Cutter(true);
        sidequests = new List<string>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    Debug.Log(step);
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            step = 54;
        }
	    if (Input.GetKeyDown(KeyCode.Alpha9))
	    {
	        SceneManager.LoadScene(5);
        }
        string str = "";
        foreach (string s in sidequests)
        {
            str += s + "\n";
        }
        side.text = str;
        if (GetComponent<State>().life <= 0)
        {
            GetComponent<State>().life = State.maxlife;
            step = 0;
            quest.text = "";
            SkipDialogue();
            transform.position = new Vector3(0, 0, 0);
        }
        if (enemycount >= 14)
        {
            speaker.color = Color.grey;
            sidequests.Remove("Kill 10 enemies\n" + (enemycount - 4).ToString() + "/10\n<color=purple>Reward: 3000$</color>");
            State.money += 3000;
            enemycount = -500;
        }
        if (MarketUpgrades.upgrades >= 4)
        {
            speaker.color = Color.grey;
            sidequests.Remove("Buy 4 upgrades");
            MarketUpgrades.upgrades -= 500;
        }
        if (NbSkills() >= 4 && !skillquest)
        {
            skillquest = true;
            speaker.color = Color.grey;
            sidequests.Remove("Unlock 4 skills");
        }
	    switch (step)
        {
            case 0:
                if (buggy >= 2 && !bug.isPlaying)
                {
                    step = 1;
                    Cutter(false);
                    Cut.color = Color.black;
                    speaker.color = Color.red;
                    ChangeDialogue("Ship" , "WARNING\nDamaged ship. AI in standby\nAwaiting pilot's instructions\nLast interaction: 67 hours ago");
                }
                else if (!bug.isPlaying)
                {
                    buggy++;
                    bug.Play();
                }
                break;
            case 1:
                if (Input.anyKeyDown)
                {
                    Cutter(false);
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "W-what's going on?");
                    step = 2;
                }
                break;
            case 2:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.red;
                    ChangeDialogue("Ship", "Pilot activity detected\nPlease move the ship around to prove you're awake");
                    step = 3;
                }
                break;
            case 3:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.grey;
                    ChangeDialogue("<i>Dashboard</i>", "New quest:\nMove around to show you're awake\n<color=purple>Reward: 750 XP, 500$</color>\nPress Tab to access it");
                    GetComponent<SpaceshipControls>().blockMovement = false;
                    GetComponent<SpaceshipControls>().blockRotation = false;
                    step = 4;
                }
                break;
            case 4:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                }
                cz = (KeyPressed(ref BeginningZ, KeyCode.Z) || cz == "green") ? "green" : "red";
                cq = (KeyPressed(ref BeginningQ, KeyCode.Q) || cq == "green") ? "green" : "red";
                cs = (KeyPressed(ref BeginningS, KeyCode.S) || cs == "green") ? "green" : "red";
                cd = (KeyPressed(ref BeginningD, KeyCode.D) || cd == "green") ? "green" : "red";
                quest.text = "Press movement keys to show you're awake (<color=" + cz + ">Z</color>, <color=" + cq + ">Q</color>, <color=" + cs + ">S</color>, <color=" + cd + ">D</color>)\n<color=purple>Reward: 750 XP, 500$</color>";
                if (cz == "green" && cq == "green" && cs == "green" && cd == "green")
                {
                    State.money += 500;
                    State.xp += 750;
                    speaker.color = Color.grey;
                    quest.text = "";
                    ChangeDialogue("<i>Dashboard</i>", "Quest completed");
                    sounded = false;
                    step = -1;
                }
                break;
            case -1:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.red;
                    ChangeDialogue("Ship", "Pilot's presence confirmed\nAI coming out of standby mode");
                    step = 5;
                }
                break;
            case 5:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    Cutter(true);
                    StartPause = Time.time;
                    step = 6;
                }
                break;
            case 6:
                if (Time.time - StartPause >= 0.0f)
                {
                    Cutter(false);
                    speaker.color = Color.red;
                    ChangeDialogue("Ship", "WARNING\nShip damaged\nAwaiting resources to make repairs");
                    step = 7;
                }
                break;
            case 7:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "What happened?");
                    step = 8;
                }
                break;
            case 8:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.red;
                    ChangeDialogue("Ship", "WARNING\nShip damaged\nAwaiting resources to make repairs");
                    step = 9;
                }
                break;
            case 9:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "Well,\nI guess there's only one thing to do...");
                    step = 10;
                }
                break;
            case 10:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.grey;
                    ChangeDialogue("<i>Dashboard</i>", "New quest:\nFind resources and shoot them to collect them\n<color=purple>Reward: 500 XP</color>");
                    quest.text = "Find resources and shoot them to collect them (0/4)\n<color=purple>Reward: 500 XP</color>";
                    Vector3 first = transform.position + transform.forward * 20 + transform.right * 8;
                    Vector3 second = transform.position + transform.forward * 20 + transform.right * (-8);
                    Vector3 third = transform.position + transform.forward * 20 + transform.up * 8;
                    Vector3 fourth = transform.position + transform.forward * 20 + transform.up * (-8);
                    Instantiate(min1,first, transform.rotation);
                    Instantiate(min2, second, transform.rotation);
                    Instantiate(min3, third, transform.rotation);
                    Instantiate(min4, fourth, transform.rotation);
                    step = 11;
                }
                break;
            case 11:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                }
                if (State.xp >= 350)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.grey;
                    quest.text = "";
                    State.xp += 500;
                    ChangeDialogue("<i>Dashboard</i>", "Quest completed");
                    step = -2;
                }
                quest.text = "Find resources and shoot them to collect them (" +((State.xp - 330) / 5).ToString() + "/4)";
                break;
            case -2:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.red;
                    quest.text = "";
                    ChangeDialogue("Ship", "Enough resources provided\nStarting reparation process");
                    step = 12;
                }
                break;
            case 12:
                quest.text = "";
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.red;
                    ChangeDialogue("Ship", "Ship repaired\nYou have one message waiting to be heard\nFrom: YOURSELF  68 hours ago");
                    step = 13;
                }
                break;
            case 13:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "I have absolutely no memory of this...\nPlease read the message");
                    step = 14;
                }
                break;
            case 14:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.red;
                    ChangeDialogue("Ship", "\"@#{ight ç[# lost\nSquad destroy`è= by ~^-(\n Find Sgt Lawson\"");
                    step = 15;
                }
                break;
            case 15:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "I didn't understand anything\nPlease replay the message");
                    step = 16;
                }
                break;
            case 16:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.red;
                    ChangeDialogue("Ship", "\"@#{ight ç[# lost\nSquad destroy`è= by ~^-(\n Find Sgt Lawson\"");
                    step = 17;
                }
                break;
            case 17:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "Damn, the message must've been damaged, \nI won't be able to get any more info");
                    step = 18;
                }
                break;
            case 18:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "Can you get the coordinates of Sgt Lawson?\nI guess he's my only chance to understand what happened");
                    step = 19;
                }
                break;
            case 19:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.red;
                    ChangeDialogue("Ship", "Sgt Lawson located\nHis position's been locked");
                    step = 20;
                }
                break;
            case 20:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.grey;
                    GetComponent<Lock>().target = Lawson.transform;
                    ChangeDialogue("<i>Dashboard</i>", "New quest:\nGo find Sgt Lawson\n<color=purple>Reward: 500 XP</color>");
                    quest.text = "Go find Sgt Lawson\n<color=purple>Reward: 500 XP</color>";
                    step = 21;
                }
                break;
            case 21:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                }
                break;
            case 22:
                quest.text = "";
                speaker.color = Color.grey;
                State.xp += 500;
                ChangeDialogue("<i>Dashboard</i>", "Quest completed");
                step = -3;
                break;
            case -3:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "Lawson?\nCan you hear me?\nWhat happened to you?");
                    step = 23;
                }
                break;
            case 23:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.blue;
                    ChangeDialogue("Sgt Lawson", "...");
                    step = 24;
                }
                break;
            case 24:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "Please answer me");
                    step = 25;
                }
                break;
            case 25:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.blue;
                    ChangeDialogue("Sgt Lawson", "Urh...\nMy head hurts so much\nWhere am I?");
                    step = 26;
                }
                break;
            case 26:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "Lawson, you're alive! I'm so relieved!\nI'm not alone, and you're not either");
                    step = 27;
                }
                break;
            case 27:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.blue;
                    ChangeDialogue("Sgt Lawson", "What's going on?\n What happened?");
                    step = 28;
                }
                break;
            case 28:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "I actually came here to ask you these questions...\nI had left a message to myself, telling me to find you");
                    step = 29;
                }
                break;
            case 29:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "According to my ship, I've been unconscious for 67 hours\nI can't remeber what happened at this moment");
                    step = 30;
                }
                break;
            case 30:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.blue;
                    ChangeDialogue("Sgt Lawson", "My ship doesn't seem to have any data prior to 70 hours ago\nSomething must have happened while our squad was patrolling");
                    step = 31;
                }
                break;
            case 31:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "We need to find the other members of the squad then!\nLet me try to locate them");
                    step = 32;
                }
                break;
            case 32:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "...");
                    step = 33;
                }
                break;
            case 33:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "I did not manage to find anyone\nHow is it possible? I hope they are fine");
                    step = 34;
                }
                break;
            case 34:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.blue;
                    ChangeDialogue("Sgt Lawson", "Listen.. My ship's been badly damaged, I don't think it can move\nBut you can find out what happened\nYou should go to our headquarters");
                    step = 35;
                }
                break;
            case 35:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.grey;
                    ChangeDialogue("<i>Dashboard</i>", "New quest:\nGo to your squad's headquarter\n<color=purple>Reward: 500 XP</color>");
                    quest.text = "Go to your squad's headquarter\n<color=purple>Reward: 500 XP</color>";
                    step = 36;
                }
                break;
            case 36:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    GetComponent<Lock>().target = HQ.transform;
                    ChangeDialogue("Pilot", "Alright\n");
                    step = 37;
                }
                break;
            case 37:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                }
                break;
            case 38:
                quest.text = "";
                speaker.color = Color.grey;
                State.xp += 500;
                GetComponent<Lock>().target = null;
                ChangeDialogue("<i>Dashboard</i>", "Quest completed");
                step = -4;
                break;
            case -4:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.red;
                    ChangeDialogue("Ship", "Connection with Headquarter established\nRetrieving data...");
                    step = 39;
                }
                break;
            case 39:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    ChangeDialogue("Ship", "Last entry: 70 hours ago, near Ixal\n\"Squad has stumbled upon an unexpected threat\nGetting ready to defend itself if necessary\"");
                    step = 40;
                }
                break;
            case 40:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "I need to go to Ixal discover exactly what happened\nBut I need to prepare for this trip...");
                    step = 41;
                }
                break;
            case 41:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.grey;
                    ChangeDialogue("<i>Dashboard</i>", "New quest:\nUnlock a skill in you skill tree (T)\n<color=purple>Reward: 500 XP</color>");
                    quest.text = "Unlock a skill in you skill tree (T)\n<color=purple>Reward: 500 XP</color>";
                    step = 42;
                }
                break;
            case 42:
                Skills sk = GetComponent<Skills>();
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                }
                if (Skills.dashUnlock || Skills.lureUnlock || Skills.missileUnlock || Skills.stealthUnlock || Skills.shieldUnlock || Skills.timeControl)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.grey;
                    State.xp += 500;
                    quest.text = "";
                    ChangeDialogue("<i>Dashboard</i>", "Quest completed");
                    step = 43;
                }
                break;
            case 43:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    initialmoney = State.money;
                    ChangeDialogue("<i>Dashboard</i>", "New quest:\nBuy something in the shop\n<color=purple>Reward: 1000$</color>");
                    quest.text = "Buy something in the shop\n<color=purple>Reward: 1000$</color>";
                    step = 44;
                }
                break;
            case 44:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                }
                if (MarketUpgrades.upgrades >= 1)
                {
                    SkipDialogue();
                    sounded = false;
                    State.money += 1000;
                    quest.text = "";
                    ChangeDialogue("<i>Dashboard</i>", "Quest completed");
                    step = -5;
                }
                break;
            case -5:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    ChangeDialogue("<i>Dashboard</i>", "<i>Side Quest:</i>\nKill 10 enemies\n<color=purple>Reward: 3000$</color>");
                    sidequests.Add("Kill 10 enemies\n" + (enemycount - 4).ToString() + "/10\n<color=purple>Reward: 3000$</color>");
                    step = 45;
                }
                break;
            case 45:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    ChangeDialogue("<i>Dashboard</i>", "Go to Ixal\n<color=purple>Reward: 250 XP, 250$</color>");
                    quest.text = "Go to Ixal\n<color=purple>Reward: 250 XP, 250$</color>";
                    GetComponent<Lock>().target = Ixal.transform;
                    step = 46;
                }
                break;
            case 46:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                }
                break;
            case 47:
                State.money += 250;
                State.xp += 250;
                quest.text = "";
                GetComponent<Lock>().target = null;
                quest.text = "";
                ChangeDialogue("<i>Dashboard</i>", "Quest completed");
                step = 48;
                break;
            case 48:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.white;
                    ChangeDialogue("???", "I've been waiting\nI knew at least one of you would come here");
                    step = 49;
                }
                break;
            case 49:
                if(Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "Who's talking?\nHow did you know I was going to come?");
                    step = 50;
                }
                break;
            case 50:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.white;
                    ChangeDialogue("???", "So many questions already\nI shall answer only once you've proven me that you're worthy");
                    step = 51;
                }
                break;
            case 51:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.white;
                    ChangeDialogue("???", "Finish this race in less than 30 seconds and I'll be convinced");
                    step = 52;
                }
                break;
            case 52:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    GetComponent<Countdown>().enabled = true;
                    GetComponent<CountCPs>().enabled = true;
                    transform.position = new Vector3(600, -190, -2085);
                    transform.rotation = new Quaternion(0, 0, -30, 0);
                    step = 53;
                }
                break;
            case 53:
                if (raced)
                {
                    Debug.Log(1);
                    GetComponent<Countdown>().enabled = false;
                    GetComponent<CountCPs>().enabled = false;
                    Debug.Log(2);
                    step = 54;
                }
                break;
            case 54:
                if (Input.anyKeyDown)
                {
                    victorypanel.SetActive(false);
                    ChangeDialogue("???", "I have to admit this was pretty impressive\nI will now explain my purpose to you");
                    step = 55;
                }
                break;
            case 55:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    ChangeDialogue("???", "I know who attacked your squad\nAnd I want to help you to get your revenge");
                    step = 56;
                }
                break;
            case 56:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "Who?\nHow do you know?\nWho are you?");
                    step = 57;
                }
                break;
            case 57:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.white;
                    ChangeDialogue("???", "I'm a mercenary\nI cannot show myself, I cannot tell anymore about myself,\nBut I can tell you that I also seek revenge against the one responsible");
                    step = 58;
                }
                break;
            case 58:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    ChangeDialogue("Mercenary", "The one you're looking for is named Orez\nHe's been terrorizing the area around my planet for years\nYou have to find him and end this, for I cannot fight him by myself");
                    step = 59;
                }
                break;
            case 59:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "I will!\nWhere can I find him?");
                    step = 60;
                }
                break;
            case 60:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.white;
                    ChangeDialogue("Mercenary", "I'll give you his coordinates right now, but you have to be fully prepared before fighting him");
                    step = 61;
                }
                break;
            case 61:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.grey;
                    ChangeDialogue("<i>Dashboard</i>", "<i>Side Quest:</i>\nUnlock 4 skills");
                    sidequests.Add("Unlock 4 skills");
                    step = 62;
                }
                break;
            case 62:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    ChangeDialogue("<i>Dashboard</i>", "<i>Side Quest:</i>\nBuy 4 upgrades");
                    sidequests.Add("Buy 4 upgrades");
                    step = 63;
                }
                break;
            case 63:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    ChangeDialogue("<i>Dashboard</i>", "Go defeat Orez");
                    step = 64;
                }
                break;
            case 64:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                }
                break;
            default:
                break;
        }
	}
    void Cutter(bool b)
    {
        if (b)
        {
            Cut.gameObject.SetActive(true);
            Cross.gameObject.SetActive(false);
            Lock.gameObject.SetActive(false);
            Health.gameObject.SetActive(false);
            Dialogue.gameObject.SetActive(false);
            Ship.GetComponent<SpaceshipControls>().blockMovement = true;
            Ship.GetComponent<SpaceshipControls>().blockMovement = true;
        }
        else
        {
            Cut.gameObject.SetActive(false);
            Cross.gameObject.SetActive(true);
            Lock.gameObject.SetActive(true);
            Health.gameObject.SetActive(true);
            Ship.GetComponent<SpaceshipControls>().blockMovement = false;
            Ship.GetComponent<SpaceshipControls>().blockMovement = false;
        }
    }
    void ChangeDialogue(string spk, string msg)
    {
        Dialogue.gameObject.SetActive(true);
        speaker.text = spk + ":";
        message.text = msg;
    }
    void SkipDialogue()
    {
        if (!sounded)
        {
            next.Play();
            Dialogue.gameObject.SetActive(false);
            sounded = true;
        }
    }
    public bool KeyPressed(ref float Beginning, KeyCode key)
    {
        if (Beginning == 0)
        {
            if (Input.GetKeyDown(key))
            {
                if (!sounded)
                {
                    next.Play();
                    sounded = true;
                }
                Dialogue.gameObject.SetActive(false);
                Beginning = Time.time;
            }
            else if (Input.anyKeyDown)
            {
                if (!sounded)
                {
                    next.Play();
                    sounded = true;
                }
                Dialogue.gameObject.SetActive(false);
            }
            return false;
        }
        else
        {
            if (Input.GetKeyUp(key))
            {
                Beginning = 0;
                return false;
            }
            else if (Time.time - Beginning >= 0.75)
                return true;
            return false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LawShield") && step == 21)
        {
            SkipDialogue();
            sounded = false;
            step = 22;
        }
        else if (other.CompareTag("HeadShield") && (step == 37))
        {
            SkipDialogue();
            sounded = false;
            step = 38;
        }
        else if (other.CompareTag("Ixal") && step == 46)
        {
            SkipDialogue();
            sounded = false;
            step = 47;
        }
        else if (other.CompareTag("FinalPortal") && (step == 64))
        {
            SceneManager.LoadScene(5);
        }
    }
    int NbSkills()
    {
        int x = 0;
        if (Skills.dashUnlock)
            x++;
        if (Skills.missileUnlock)
            x++;
        if (Skills.missileUnlock)
            x++;
        if (Skills.shieldUnlock)
            x++;
        if (Skills.timeControl)
            x++;
        if (Skills.stealthUnlock)
            x++;
        return x;
    }
}
