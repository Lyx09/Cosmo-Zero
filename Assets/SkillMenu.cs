using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillMenu : MonoBehaviour
{

    public GameObject EscapePanel;
    public SpaceshipControls movements;
    public GameObject player;
    public Text text;

    void Start()
    {
        EscapePanel.gameObject.SetActive(false);
        movements = GetComponent<SpaceshipControls>();
    }

    void Update()
    {
        State s = player.GetComponent<State>();
        text.text = "SKILLTREE \n REMAINING SKILLPOINTS : " + s.skillpoints.ToString();

        if (Input.GetKeyDown("l"))
        {
            if (EscapePanel.gameObject.activeSelf)
            {
                movements.blockMovement = false;
                movements.blockRotation = false;
                EscapePanel.gameObject.SetActive(false);
            }
            else
            {
                movements.blockMovement = true;
                movements.blockRotation = true;
                EscapePanel.gameObject.SetActive(true);
            }
        }
    }
}
