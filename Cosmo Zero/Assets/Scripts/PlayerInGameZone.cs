using UnityEngine;
using System.Collections;

public class PlayerInGameZone : MonoBehaviour
{
    private GameObject gameZone;

	// Use this for initialization
	void Start () {
	gameZone = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
        //Check position of ship
        //Gives a warning if ship goes too far or else destroy the ship or whatever
    }
}
