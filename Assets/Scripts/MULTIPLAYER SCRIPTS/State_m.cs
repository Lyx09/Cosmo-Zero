using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class State_m : MonoBehaviour
{
    public float maxHealth = 1000;

    public float currentHealth = 1000;

    public bool destroyOnDeath = false;

    public float regenlife;
    public float cdregen; //Le temps qu'il faut passer hors combat avant de passer en mode régen
    public float timeregen; //Le temps entre chaque tick de régen

    public int kills = 0;
    public int deaths = 0;

    private float chrono; //L'heure à partir de laquelle on compte quand est-ce qu'on pourra régen
    private float cooldown = 5.0F; //Le temps à attendre avant le prochain gain de vie

    public Text LifeDisp;
    public Image foreground;
    private Transform[] spawnPoints;

    void Start ()
    {
        currentHealth = maxHealth;
        kills = 0;
        deaths = 0;

        spawnPoints = new Transform[0];
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

        // Keep the HUD in sync (previously driven by SyncVar hooks)
        UpdateDisplayHealth(currentHealth);
	}

    public void TakeDamage(float amount)
    {
        Debug.Log(amount);

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                deaths++;
                currentHealth = maxHealth; //waiting time
                RpcRespawn();
            }
        }

        chrono = Time.time;
        cooldown = cdregen;
    }

    void UpdateDisplayHealth(float curHealth)
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

    void UpdateDisplayDeaths(int dth)
    {
        if (foreground != null)
        {
            foreground.fillAmount = currentHealth / maxHealth;
        }

        if (LifeDisp != null)
        {
            LifeDisp.text = " HP: " + ((int)currentHealth).ToString() + "\n Kills: " + kills.ToString() + "\n Deaths: " + dth.ToString();
        }
    }

    void UpdateDisplayKills(int kls)
    {
        if (foreground != null)
        {
            foreground.fillAmount = currentHealth / maxHealth;
        }

        if (LifeDisp != null)
        {
            LifeDisp.text = " HP: " + ((int)currentHealth).ToString() + "\n Kills: " + kls.ToString() + "\n Deaths: " + deaths.ToString();
        }
    }

    void RpcRespawn()
    {
        // Set the spawn point to origin as a default value
        Vector3 spawnPoint = Vector3.zero;

        // If there is a spawn point array and the array is not empty, pick one at random
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
        }

        // Set the player’s position to the chosen spawn point
        transform.position = spawnPoint;
    }
}
