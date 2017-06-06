using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class State : MonoBehaviour
{
    public float maxlife; //La vie max. Obvious comment is obvious
    public float regenlife; //Quantité de vie régen à chaque régen
    public Text LifeDisp; //L'objet UI qui display TOUT
    public float cdregen; //Le temps qu'il faut passer hors combat avant de passer en mode régen
    public float timeregen; //Le temps entre chaque tips de régen
    private int money;
    public int xp;
    public float life; //Bah...
    private float chrono; //L'heure à partir de laquelle on compte quand est-ce qu'on pourra régen
    private float cooldown; //Le temps à attendre avant le prochain gain de vie
    public int level = 1;
    public int skillpoints;
	// Use this for initialization
	void Start ()
    {
        life = maxlife;
        money = 0;
        xp = 0;
        UpLife();
        cooldown = 5.00F;
        level = 1;
	}

    // Update is called once per frame
    void Update()
    {
        if (xp >= 250 * Mathf.Pow(2,level))
            xp = levelup(xp);
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Hurt(10);
            }
        }
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



    public int levelup (int xp)
    {        
        level += 1;
        skillpoints += 1;
        xp -= 125 * (int)Mathf.Pow(2, level);
        return xp;
    }

    public void Hurt (float power) //A appeler quand le vaisseau est touché
    {
        if (life - power <= maxlife)
            life -= power;
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
        LifeDisp.text = " HP: " + ((int)life).ToString() + " / " + ((int)maxlife).ToString() +"\n Money: " + money.ToString() + "\n XP: " + xp.ToString() + " / " + (250 * Mathf.Pow(2, level)).ToString() + "\n Level: " + level.ToString();
    }
}
