using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class bossQuest : MonoBehaviour
{
    public GameObject Dialogue;
    public Text speaker, message;

    public int step = 0;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    switch (step)
	    {
            case 0:
                //ChangeDialogue();
                break;
	    }
	}

    void ChangeDialogue(string spk, string msg)
    {
        Dialogue.gameObject.SetActive(true);
        speaker.text = spk + ":";
        message.text = msg;
    }
}
