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
    public static int upgrades = 0;


    public void LifeUpgrade()
    {
        State s = player.GetComponent<State>();
        if (State.money >= LifeUpCost)
        {
            upgrades++;
            State.money -= LifeUpCost;
            LifeUpCost *= 2;
            State.maxlife *= 2;
            text.text = "LIFE UPGRADE\n" + (State.maxlife).ToString() + " HPmax to " + (State.maxlife * 2).ToString() + " HPmax\n COST" + LifeUpCost.ToString();
            if (State.maxlife > 5000)
            {
                button.GetComponent<Button>().interactable = false;
                text.text = "MAX LEVEL REACHED";    
            }
        }
    }
    public void RegenUpgrade()
    {
        State s = player.GetComponent<State>();
        if (State.money >= RegenUpCost)
        {
            upgrades++;
            if (State.regenlife == 0)
                State.regenlife = 1;
            else
                State.regenlife += 1;
            State.money -= RegenUpCost;
            RegenUpCost *= 2;
            text.text = "REGEN UPGRADE\n" + (State.regenlife).ToString() + " HP/sec to " + (State.regenlife + 1).ToString() + " HP/sec\n COST" + RegenUpCost.ToString();
            if (State.regenlife == 5)
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
        if (State.money >= FireRateUpCost)
        {
            upgrades++;
            State.money -= FireRateUpCost;
            FireRateUpCost *= 2;
            Shooting.cd -= (float).1;
            text.text = "RATE OF FIRE UPGRADE\n" + (Shooting.cd).ToString() + " to " + (Shooting.cd - 0.1).ToString() + "\n COST" + (FireRateUpCost /2).ToString();
            if (Shooting.cd == 0.2)
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
        if (State.money >= DamageUpCost)
        {
            upgrades++;
            State.money -= DamageUpCost;
            DamageUpCost *= 2;
            Shooting.damage += 1;
            text.text = "DAMAGE UPGRADE\n" + (Shooting.damage).ToString() + " damage to " + (Shooting.damage +1).ToString() + " damage\n COST " + DamageUpCost.ToString();
            if (Shooting.damage == 8)
            {
                button.GetComponent<Button>().interactable = false;
                text.text = "MAX LEVEL REACHED";
            }
        }
    }
}