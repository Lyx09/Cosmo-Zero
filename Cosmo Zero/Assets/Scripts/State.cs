﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class State : MonoBehaviour {
    public int maxlife; //La vie max. Obvious comment is obvious
    public int regenlife; //Quantité de vie régen à chaque régen
    public Text LifeDisp; //L'objet UI qui display les HP
    public float cdregen; //Le temps qu'il faut passer hors combat avant de passer en mode régen
    public float timeregen; //Le temps entre chaque tips de régen
    private int life; //Bah...
    private float chrono; //L'heure à partir de laquelle on compte quand est-ce qu'on pourra régen
    private float cooldown; //Le temps à attendre avant le prochain gain de vie
	// Use this for initialization
	void Start ()
    {
        life = maxlife;
        UpLife();
        cooldown = 5.00F;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Hurt(2);
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

    void Hurt (int power) //A appeler quand le vaisseau est touché
    {
        life -= power;
        chrono = Time.time;
        cooldown = cdregen;
        UpLife();
    }

    void UpLife ()
    {
        LifeDisp.text = "HP: " + life.ToString();
    }
}
