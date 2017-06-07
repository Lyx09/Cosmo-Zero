using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public int delai;
    public Text text;
    public GameObject panel;
    private float beginning;
    public GameObject chrono;
    public 

    void Start ()
    {
        beginning = Time.time;
        chrono.SetActive(true);
    }
    void Update()
    {
        float t = Time.time;
        text.text = ((int)delai - (t - beginning)).ToString();
        if ((t-beginning) > delai)
            panel.SetActive(true);
        if ((t-beginning) > (delai + 3))
        {
            panel.SetActive(false);
            transform.position = new Vector3(600, -190, -2085);
            beginning = Time.time;
        }
    }
}