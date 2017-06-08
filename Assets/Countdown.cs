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
        if ((t - beginning) > delai)
        {
            panel.SetActive(true);
            GetComponent<CountCPs>().CP = false;
            GetComponent<CountCPs>().CP1 = false;
            GetComponent<CountCPs>().CP2 = false;
            GetComponent<CountCPs>().CP3 = false;
            GetComponent<CountCPs>().CP4 = false;
        }
        if ((t-beginning) > (delai + 3))
        {
            panel.SetActive(false);
            transform.position = new Vector3(600, -190, -2085);
            transform.rotation = new Quaternion(0, 0, -30, 0);
            beginning = Time.time;
        }
    }
}