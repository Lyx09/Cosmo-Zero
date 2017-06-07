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
                movements.blockMovement = false;
                movements.blockRotation = false;
                SkillMenuPanel.gameObject.SetActive(false);
            }
            else if(!MarketPanel.gameObject.activeSelf && !EscapeMenuPanel.gameObject.activeSelf)
            {
                movements.blockMovement = true;
                movements.blockRotation = true;
                SkillMenuPanel.gameObject.SetActive(true);
            }
        }
    }
}
