using UnityEngine;
using System.Collections;

public class SkillMenu : MonoBehaviour
{

    public GameObject EscapePanel;
    public SpaceshipControls movements;

    void Start()
    {
        EscapePanel.gameObject.SetActive(false);
        movements = GetComponent<SpaceshipControls>();
    }

    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            if (EscapePanel.gameObject.activeSelf)
            {
                movements.blockMovement = true;
                movements.blockRotation = true;
                EscapePanel.gameObject.SetActive(false);
            }
            else
            {
                movements.blockMovement = false;
                movements.blockRotation = false;
                EscapePanel.gameObject.SetActive(true);
            }
        }
    }
}
