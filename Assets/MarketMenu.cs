using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MarketMenu : MonoBehaviour
{

    public GameObject SkillMenuPanel;
    private SpaceshipControls movements;
    public GameObject player;
    public Text text;
    public GameObject EscapeMenuPanel;
    public GameObject MarketPanel;
    public GameObject health;

    void Start()
    {
        MarketPanel.gameObject.SetActive(false);
        movements = GetComponent<SpaceshipControls>();
    }

    void Update()
    {
        State s = player.GetComponent<State>();
        text.text = "SHOP \n MONEY : " + State.money.ToString();

        if (Input.GetKeyDown("y"))
        {
            if (MarketPanel.gameObject.activeSelf)
            {
                health.SetActive(true);
                movements.blockMovement = false;
                movements.blockRotation = false;
                Shooting shooting = player.GetComponent<Shooting>();
                shooting.enabled = true;
                MarketPanel.gameObject.SetActive(false);
            }
            else if (!SkillMenuPanel.gameObject.activeSelf && !EscapeMenuPanel.gameObject.activeSelf)
            {
                health.SetActive(false);
                movements.blockMovement = true;
                movements.blockRotation = true;
                Shooting shooting = player.GetComponent<Shooting>();
                shooting.enabled = false;
                MarketPanel.gameObject.SetActive(true);
            }
        }
    }
}