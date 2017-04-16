using UnityEngine;
using System.Collections;

public class QuitOnClick : MonoBehaviour {
    public void Quit ()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
