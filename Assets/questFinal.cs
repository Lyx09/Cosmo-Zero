using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class questFinal : MonoBehaviour
{
    public GameObject Dialogue;
    public Text speaker, message, header;
    public AudioSource next;

    public State state;
    public Transform core;

    public bool lost = false;
    public int step = 0;
    
    void Update()
    {

        if (lost)
        {
            step = 99;
        }

        if (state.life <= 0 && State.maxlife != 0)
        {
            step = 99;
        }


        switch (step)
        {
            case 0:
                speaker.color = Color.white;
                ChangeDialogue("Mercenary", "Now that you are in you need to destroy the core and save the cosmo!");
                if (Input.anyKeyDown)
                {
                    next.Play();
                    step = 10;
                    Dialogue.SetActive(false);
                }
                break;
            case 10:
                speaker.color = Color.white;
                header.text = "Find the core";
                ChangeDialogue("Mercenary", "Watch out there maybe still some turret inside, get the hell out of this maze and find the core!");
                if (Input.anyKeyDown)
                {
                    next.Play();
                    step = 20;
                    Dialogue.SetActive(false);
                }
                break;
            case 20:
                
                if ((core.position - transform.position).magnitude < 50)
                {
                    speaker.color = Color.white;
                    header.text = "Destroy Orez";
                    ChangeDialogue("Mercenary", "Here it is! it is our only chance, destroy that core and Orez with it !");
                    if (Input.anyKeyDown)
                    {
                        next.Play();
                        step = 30;
                        Dialogue.SetActive(false);
                    }
                }
                break;

            case 30:
                if (core == null)
                {
                    header.fontSize = 55;
                    header.color = Color.cyan;
                    header.text = "GG EZ";
                    speaker.color = Color.white;
                    ChangeDialogue("Mercenary", "Good job you did it! We are all saved!");
                    if (Input.anyKeyDown)
                    {
                        next.Play();
                        step = 40;
                        Dialogue.SetActive(false);
                    }
                }

                break;
            case 40:
                speaker.color = Color.red;
                ChangeDialogue("Developers", "Orez was defeated and so the galaxy was saved. Thank you for playing!");

                if (Input.anyKeyDown)
                {
                    next.Play();
                    step = 50;
                    Dialogue.SetActive(false);
                }
                break;

            case 99:
                speaker.color = Color.white;
                ChangeDialogue("Mercenary", "You failed the mission!");
                header.color = Color.red;
                header.text = "Mission failed";
                if (Input.anyKeyDown)
                {
                    next.Play();
                    state.life = State.maxlife;
                    lost = false;
                    step = 999;
                    Dialogue.SetActive(false);
                }
                break;

            case 999:
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
                break;
        }
    }

    public void ChangeDialogue(string spk, string msg)
    {
        Dialogue.gameObject.SetActive(true);
        speaker.text = spk + ":";
        message.text = msg;
    }
}
