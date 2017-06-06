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

    void Start()
    {
        MarketPanel.gameObject.SetActive(false);
        movements = GetComponent<SpaceshipControls>();
    }

    void Update()
    {
        State s = player.GetComponent<State>();
        text.text = "MARKET \n MONEY : " + s.money.ToString();

        if (Input.GetKeyDown("y"))
        {
            if (MarketPanel.gameObject.activeSelf)
            {
                movements.blockMovement = false;
                movements.blockRotation = false;
                MarketPanel.gameObject.SetActive(false);
            }
            else if (!SkillMenuPanel.gameObject.activeSelf && !EscapeMenuPanel.gameObject.activeSelf)
            {
                movements.blockMovement = true;
                movements.blockRotation = true;
                MarketPanel.gameObject.SetActive(true);
            }
        }
    }
}