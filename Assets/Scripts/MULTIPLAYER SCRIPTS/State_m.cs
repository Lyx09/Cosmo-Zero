using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class State_m : NetworkBehaviour
{
    public float maxHealth = 1000;

    [SyncVar(hook = "UpdateDisplay")] public float currentHealth = 1000;

    public float regenlife;
    
    public float cdregen; //Le temps qu'il faut passer hors combat avant de passer en mode régen
    public float timeregen; //Le temps entre chaque tick de régen

    private int kills = 0;
    public int deaths = 0;

    private float chrono; //L'heure à partir de laquelle on compte quand est-ce qu'on pourra régen
    private float cooldown; //Le temps à attendre avant le prochain gain de vie

    public Text LifeDisp;
    public Image foreground;

    void Start ()
    {
        currentHealth = maxHealth;
        kills = 0;
        deaths = 0;
        cooldown = 5.00F;
	}
	
	void Update ()
    {
        if (Time.time - chrono >= cooldown)
        {
            chrono = Time.time;
            currentHealth += regenlife;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            cooldown = timeregen;
        }
	}

    public void TakeDamage(float amount)
    {
        if (!isServer)
        {
            return;
        }

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth; //waiting time
            RpcRespawn();
        }

        chrono = Time.time;
        cooldown = cdregen;
        //UpdateDisplay(); //Automatically called when health changes
    }

    void UpdateDisplay(float curHealth)
    {
        if (foreground != null)
        {
            foreground.fillAmount = curHealth / maxHealth;
        }

        if (LifeDisp != null)
        {
            LifeDisp.text = " HP: " + ((int)currentHealth).ToString() + "\n Kills: " + kills.ToString() + "\n Deaths: " + deaths.ToString();
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = Vector3.zero;
        }
    }
}
