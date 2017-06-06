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
    public GameObject player;
    public GameObject button;


    public void Unlockdash()
    {
        State s = player.GetComponent<State>();
        Skills sk = player.GetComponent<Skills>();
        if (s.skillpoints >= Dashcost)
        {
            s.skillpoints -= Dashcost;
            sk.dashUnlock = true;
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
            sk.timeControl = true;
            button.GetComponent<Button>().interactable = false;
        }
    }
    public void UnlockHommingMissil()
    {
        State s = player.GetComponent<State>();
        Shooting sh = player.GetComponent<Shooting>();
        if (s.skillpoints > Dashcost)
        {
            s.skillpoints -= Dashcost;
            button.GetComponent<Button>().interactable = false;
        }
    }
}