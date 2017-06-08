using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bossQuest : MonoBehaviour
{
    public GameObject Dialogue;
    public Text speaker, message,header;
    public AudioSource next;

    public Transform turret1, turret2, turret3, turret4, turret5, turret6, turret7;

    public GameObject door1, door2;

    public Transform boss;

    public State state;

    public bool lost = false;
    public int step = 0;
    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

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
                ChangeDialogue("Mercenary", "Here you are! It seems the mother ship is trying to escape, destroy it before it escape and destroys other galaxies !");
                if (Input.anyKeyDown)
                {
                    next.Play();
                    step = 10;
                    Dialogue.SetActive(false);
                }
                break;
            case 10:
                speaker.color = Color.white;
                header.text = "Get closer to the ship";
                ChangeDialogue("Mercenary", "Quick get closer before it escapes by the jumpgate!");
                if (Input.anyKeyDown)
                {
                    next.Play();
                    step = 20;
                    Dialogue.SetActive(false);
                }
                break;
	        case 20:
	            if ((boss.position - transform.position).magnitude < 300)
	            {
	                speaker.color = Color.white;
	                header.text = "Destroy 7 turrets";
	                ChangeDialogue("Mercenary", "Now destroy all its turret then we'll see what we can do!");
	                if (Input.anyKeyDown)
	                {
	                    next.Play();
	                    step = 30;
	                    Dialogue.SetActive(false);
                    }
	            }
	            break;

	        case 30:
	            int count = 0;
	            if (turret1 == null)
	                count++;
	            if (turret2 == null)
	                count++;
	            if (turret3 == null)
	                count++;
	            if (turret4 == null)
	                count++;
	            if (turret5 == null)
	                count++;
	            if (turret6 == null)
	                count++;
	            if (turret7 == null)
	                count++;

	            if (count == 7)
	            {
	                header.text = "";
	                step = 40;
	            }
	            else
	            {
	                header.text = "Destroy " + count + " turrets";
                }

                break;
            case 40:
                speaker.color = Color.white;
                ChangeDialogue("Mercenary", "The mothership is weakening, try to destroy it from the inside!");
                header.text = "Get behind the ship and destroy it from the inside";

                Destroy(door1);
                Destroy(door2);

                if (Input.anyKeyDown)
                {
                    next.Play();
                    step = 50;
                    Dialogue.SetActive(false);
                }
                break;

            case 99:
                speaker.color = Color.white;
                ChangeDialogue("Mercenary", "The mothership fled you failed the mission!");
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
