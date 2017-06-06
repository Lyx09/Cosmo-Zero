using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class MarketUpgrades : MonoBehaviour
{
    public int LifeUpCost;
    public int RegenUpCost;
    public int DamageUpCost;
    public int FireRateUpCost;
    public Text text;
    public GameObject player;
    public GameObject button;


    public void LifeUpgrade()
    {
        State s = player.GetComponent<State>();
        if (s.money >= LifeUpCost)
        {
            s.money -= LifeUpCost;
            LifeUpCost *= 2;
            s.maxlife *= 2;
            text.text = "LIFE UPGRADE\n" + (s.maxlife).ToString() + " HP to " + (s.maxlife * 2).ToString() + " HP\n COST" + LifeUpCost.ToString();
            if (s.maxlife > 5000)
            {
                button.GetComponent<Button>().interactable = false;
                text.text = "MAX LEVEL REACHED";    
            }
        }
    }
    public void RegenUpgrade()
    {
        State s = player.GetComponent<State>();
        if (s.money >= RegenUpCost)
        {
            if (s.regenlife == 0)
                s.regenlife = 1;
            else
                s.regenlife += 1;
            s.money -= RegenUpCost;
            RegenUpCost *= 2;
            text.text = "REGEN UPGRADE\n" + (s.regenlife).ToString() + " HP/sec to " + (s.regenlife + 1).ToString() + " HP/sec\n COST" + RegenUpCost.ToString();
            if (s.regenlife == 5)
            {
                button.GetComponent<Button>().interactable = false;
                text.text = "MAX LEVEL REACHED";
            }
        }
    }
    public void FireRateUpgrade()
    {
        State s = player.GetComponent<State>();
        Shooting sh = player.GetComponent<Shooting>();
        if (s.money >= FireRateUpCost)
        {
            s.money -= FireRateUpCost;
            FireRateUpCost *= 2;
            sh.cd -= (float).1;
            text.text = "RATE OF FIRE UPGRADE\n" + (sh.cd).ToString() + " to " + (sh.cd - 0.1).ToString() + "\n COST" + (FireRateUpCost /2).ToString();
            if (sh.cd == 0.2)
            {
                button.GetComponent<Button>().interactable = false;
                text.text = "MAX LEVEL REACHED";
            }
        }
    }
    public void DamageUpgrade()
    {
        State s = player.GetComponent<State>();
        Shooting sh = player.GetComponent<Shooting>();
        if (s.money >= DamageUpCost)
        {
            s.money -= DamageUpCost;
            DamageUpCost *= 2;
            sh.damage += 1;
            text.text = "DAMAGE UPGRADE\n" + (sh.damage).ToString() + " damage to " + (sh.damage +1).ToString() + " damage\n COST " + DamageUpCost.ToString();
            if (sh.damage == 8)
            {
                button.GetComponent<Button>().interactable = false;
                text.text = "MAX LEVEL REACHED";
            }
        }
    }
}