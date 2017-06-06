using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Collections;

public class FullSolo : MonoBehaviour
{
    private bool sounded = false;
    private int step = 1;
    public GameObject Dialogue, Ship, Health, Lock, Cross;
    public Image Cut;
    public Text speaker, message, quest;
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

    // Use this for initialization
    void Start ()
    {
        Player = GetComponent<Rigidbody>();
        Cutter(true);
        GetComponent<SpaceshipControls>().blockMovement = true;
        GetComponent<SpaceshipControls>().blockRotation = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GetComponent<State>().life <= 0)
        {
            GetComponent<State>().life = GetComponent<State>().maxlife;
            step = 0;
            quest.text = "";
            SkipDialogue();
            Cutter(true);
            transform.position = new Vector3(0, 0, 0);
            Cutter(false);
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
                    ChangeDialogue("Ship" , "WARNING\nDamaged ship. IA in standby\nAwaiting pilot's instructions\nLast interaction: 67 hours ago");
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
                    ChangeDialogue("<i>Dashboard</i>", "New quest:\nPress Tab to access it");
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
                quest.text = "Press movement keys to show you're awake (<color=" + cz + ">Z</color>, <color=" + cq + ">Q</color>, <color=" + cs + ">S</color>, <color=" + cd + ">D</color>)";
                if (cz == "green" && cq == "green" && cs == "green" && cd == "green")
                {
                    speaker.color = Color.red;
                    ChangeDialogue("Ship", "Pilot's presence confirmed\nShip IA coming out of standby mode");
                    sounded = false;
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
                if (Time.time - StartPause >= 1.0f)
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
                    ChangeDialogue("<i>Dashboard</i>", "New quest:\nFind resources and shoot them to collect them");
                    quest.text = "Find resources and shoot them to collect them (0/4)";
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
                if (GetComponent<State>().xp >= 20)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.red;
                    quest.text = "";
                    ChangeDialogue("Ship", "Enough resources provided\nStarting reparation process");
                    step = 12;
                }
                quest.text = "Find resources and shoot them to collect them (" +(GetComponent<State>().xp / 5).ToString() + "/4)";
                break;
            case 12:
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
                    ChangeDialogue("<i>Dashboard</i>", "New quest:\nGo find Sgt Lawson");
                    quest.text = "Go find Sgt Lawson";
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
                speaker.color = Color.green;
                ChangeDialogue("Pilot", "Lawson?\nCan you hear me?\nWhat happened to you?");
                step = 23;
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
                    ChangeDialogue("Sgt Lawson", "My ship's data seems to indicate that an unexpected threat appeared 70 hours ago\nOur whole squad was moving in a zone supposed to be safe\nI'm afraid we've been attacked");
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
                    ChangeDialogue("<i>Dashboard</i>", "New quest:Go to your squad's headquarters");
                    quest.text = "Go to your squad's headquarters";
                    step = 36;
                }
                break;
            case 36:
                if (Input.anyKeyDown)
                {
                    SkipDialogue();
                    sounded = false;
                    speaker.color = Color.green;
                    ChangeDialogue("Pilot", "Alright\n");
                    step = 37;
                }
                break;
            case 37:
                if (Input.anyKeyDown)
                {
                    
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
    }
}
