using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FullSolo : MonoBehaviour
{
    private bool sounded = false;
    private int step = 0;
    public GameObject Dialogue, Cut, Ship, Health, Lock, Cross;
    public Text message;
    public Text quest;
    private Rigidbody Player;
    public AudioSource next, bug;
    private int buggy = 0;

    // Use this for initialization
    void Start ()
    {
        Player = GetComponent<Rigidbody>();
        Cut.gameObject.SetActive(true);
        Cross.gameObject.SetActive(false);
        Lock.gameObject.SetActive(false);
        Ship.GetComponent<SpaceshipControls>().blockMovement = true;
        Ship.GetComponent<SpaceshipControls>().blockMovement = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    switch (step)
        {
            case 0:
                if (buggy >= 2 && !bug.isPlaying)
                {
                    step = 1;
                    Cut.gameObject.SetActive(false);
                    Health.gameObject.SetActive(true);
                    Dialogue.gameObject.SetActive(true);
                    Cross.gameObject.SetActive(true);
                    Lock.gameObject.SetActive(true);
                    Ship.GetComponent<SpaceshipControls>().blockMovement = false;
                    Ship.GetComponent<SpaceshipControls>().blockMovement = false;
                }
                else if (!bug.isPlaying)
                {
                    buggy++;
                    bug.Play();
                }
                break;
            default:
                break;
        }
	}
}
