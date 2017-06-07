using UnityEngine;
using System.Collections;

public class StoreInfo : MonoBehaviour
{
    public bool first;
    public static bool dashUnlock;
    public static bool shieldUnlock;
    public static bool timecontrol;
    public static bool stealthunlock;
    public static bool lureunlock;
    public static bool missileunlock;
    public static int dmg;
    public static float rate;
    public static float hp;
    public static float regen;
	// Use this for initialization
	void Start ()
    {
	    if (!first)
        {
            Skills sk = GetComponent<Skills>();
            
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
