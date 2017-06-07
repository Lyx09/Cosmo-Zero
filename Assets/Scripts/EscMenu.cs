using UnityEngine;
using System.Collections;

public class EscMenu : MonoBehaviour
{

    public GameObject EscapePanel;
    public GameObject SkillMenuPanel;
    public GameObject MarketPanel;
    private SpaceshipControls movements;
    public GameObject player;

    void Start()
    {
        EscapePanel.gameObject.SetActive(false);
        movements = GetComponent<SpaceshipControls>();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (EscapePanel.gameObject.activeSelf)
            {
                movements.blockMovement = false;
                movements.blockRotation = false;
                Shooting shooting = player.GetComponent<Shooting>();
                shooting.enabled = true;
                EscapePanel.gameObject.SetActive(false);
            }
            else if (!MarketPanel.gameObject.activeSelf && !SkillMenuPanel.gameObject.activeSelf)
            {

                movements.blockMovement = true;
                movements.blockRotation = true;
                Shooting shooting = player.GetComponent<Shooting>();
                shooting.enabled = false;
                EscapePanel.gameObject.SetActive(true);
            }
        }
    }
}