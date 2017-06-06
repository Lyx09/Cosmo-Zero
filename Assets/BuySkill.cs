using UnityEngine;
using System.Collections;

public class BuySkill : MonoBehaviour {
    public int Dashcost;
    public int Timefreezecost;
    public GameObject player;
    public GameObject button;

    void Start()
    {
    }
    void Unlockdash()
    {
        State s = player.GetComponent<State>();
        Skills sk = player.GetComponent<Skills>();
        int xp = s.xp;
        if (xp > Dashcost)
        {
            xp -= Dashcost;
            button.SetActive(false);
            sk.dashUnlock = true;
        }
    }
    void UnlockTimefreeze()
    {
        State s = player.GetComponent<State>();
        Skills sk = player.GetComponent<Skills>();
        int xp = s.xp;
        if (xp > Timefreezecost)
        {
            xp -= Timefreezecost;
            button.SetActive(false);
            sk.timeControl = true;
        }
    }
}
