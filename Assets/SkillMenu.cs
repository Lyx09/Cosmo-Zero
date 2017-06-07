using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillMenu : MonoBehaviour
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
        SkillMenuPanel.gameObject.SetActive(false);
        movements = GetComponent<SpaceshipControls>();
    }

    void Update()
    {
        State s = player.GetComponent<State>();
        text.text = "SKILLTREE \n REMAINING SKILLPOINTS : " + s.skillpoints.ToString();

        if (Input.GetKeyDown("t"))
        {
            if (SkillMenuPanel.gameObject.activeSelf)
            {
                health.SetActive(true);
                movements.blockMovement = false;
                movements.blockRotation = false;
                Shooting shooting = player.GetComponent<Shooting>();
                shooting.enabled = true;
                SkillMenuPanel.gameObject.SetActive(false);
            }
            else if(!MarketPanel.gameObject.activeSelf && !EscapeMenuPanel.gameObject.activeSelf)
            {
                health.SetActive(false);
                movements.blockMovement = true;
                movements.blockRotation = true;
                Shooting shooting = player.GetComponent<Shooting>();
                shooting.enabled = false;
                SkillMenuPanel.gameObject.SetActive(true);
            }
        }
    }
}
