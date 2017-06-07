using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuySkill : MonoBehaviour
{
    public int Dashcost;
    public int Timefreezecost;
    public int Hommingmissilecost;
    public int stealthcost;
    public int shieldcost;
    public int Lurecost;
    public GameObject player;
    public GameObject button;


    public void Unlockdash()
    {
        State s = player.GetComponent<State>();
        Skills sk = player.GetComponent<Skills>();
        if (s.skillpoints >= Dashcost)
        {
            s.skillpoints -= Dashcost;
            Skills.dashUnlock = true;
            button.GetComponent<Button>().interactable = false;
        }
    }
    public void UnlockTimefreeze()
    {
        State s = player.GetComponent<State>();
        Skills sk = player.GetComponent<Skills>();
        if (s.skillpoints >= Timefreezecost)
        {
            s.skillpoints -= Timefreezecost;
            Skills.timeControl = true;
            button.GetComponent<Button>().interactable = false;
        }
    }
    public void UnlockHommingMissil()
    {
        State s = player.GetComponent<State>();
        Skills sk = player.GetComponent<Skills>();
        if (s.skillpoints >= Hommingmissilecost)
        {
            s.skillpoints -= Hommingmissilecost;
            Skills.missileUnlock = true;
            button.GetComponent<Button>().interactable = false;
        }
    }
    public void UnlockStealthMode()
    {
        State s = player.GetComponent<State>();
        Skills sk = player.GetComponent<Skills>();
        if (s.skillpoints >= stealthcost)
        {
            s.skillpoints -= stealthcost;
            Skills.stealthUnlock = true;
            button.GetComponent<Button>().interactable = false;
        }
    }
    public void UnlockShield()
    {
        State s = player.GetComponent<State>();
        Skills sk = player.GetComponent<Skills>();
        if (s.skillpoints >= shieldcost)
        {
            s.skillpoints -= shieldcost;
            Skills.shieldUnlock = true;
            button.GetComponent<Button>().interactable = false;
        }
    }
    public void Unlocklures()
    {
        State s = player.GetComponent<State>();
        Skills sk = player.GetComponent<Skills>();
        if (s.skillpoints >= Lurecost)
        {
            s.skillpoints -= Lurecost;
            Skills.lureUnlock = true;
            button.GetComponent<Button>().interactable = false;
        }
    }
}