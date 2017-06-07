using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        if (GameObject.Find("Network Manager") != null)
        {
            Destroy(GameObject.Find("Network Manager"));
        }
    }
}
