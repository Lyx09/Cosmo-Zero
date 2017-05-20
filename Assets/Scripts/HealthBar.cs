using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject player;
    public Image background;
    public Image foreground;
    private State s;
	// Use this for initialization
	void Start ()
    {
        Color c = background.color;
        c.a = 0.3F;
        background.color = c;
        s = player.GetComponent<State>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        foreground.fillAmount = s.life / s.maxlife;
	}
}
