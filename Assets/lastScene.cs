using UnityEngine;
using System.Collections;
using  UnityEngine.SceneManagement;

public class lastScene : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene("BossInside");
    }
}
