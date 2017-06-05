using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class State : MonoBehaviour
{
    //Display
    public Text LifeDisp; //L'objet UI qui display TOUT
    public float life = 100; //Bah...
    public float maxlife; //La vie max. Obvious comment is obvious
    private int money;
    public int xp;
    //Regen
    public float regenlife; //Quantité de vie régen à chaque régen
    public float cdregen; //Le temps qu'il faut passer hors combat avant de passer en mode régen
    public float timeregen; //Le temps entre chaque tips de régen
    private float chrono; //L'heure à partir de laquelle on compte quand est-ce qu'on pourra régen
    private float cooldown = 5.0f; //Le temps à attendre avant le prochain gain de vie

    public float shieldblock = 0;
    public GameObject shield;

    // Use this for initialization
    void Start ()
    {
        life = maxlife;
        money = 0;
        xp = 0;
        UpLife();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time - chrono >= cooldown)
        {
            chrono = Time.time;
            life += regenlife;
            if (life > maxlife)
            {
                life = maxlife;
            }
            cooldown = timeregen;
        }
        UpLife();
	}

    public void Hurt (float power) //A appeler quand le vaisseau est touché
    {
        if (power > 0)
        {
            if (shieldblock > 0)
            {
                if (shieldblock > power)
                {
                    shieldblock -= power;
                }
                else
                {
                    life -= (power - shieldblock);
                    shieldblock = 0;
                    shield.gameObject.SetActive(false);
                }
            }
            else
            {
                life -= power;
            }
        }
        if (life > maxlife)
            life = maxlife;
        chrono = Time.time;
        cooldown = cdregen;
        UpLife();
    }

    public void Kill(int a)
    {
        xp += a;
        UpLife();
    }

    void UpLife ()
    {
        LifeDisp.text = " HP: " + ((int)life).ToString() + " / " + ((int)maxlife).ToString() +"\n Money: " + money.ToString() + "\n XP: " + xp.ToString() ;
    }
}
