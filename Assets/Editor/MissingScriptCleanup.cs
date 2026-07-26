using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

/// <summary>
/// Editor utility to remove leftover "missing script" components
/// (e.g. the old UNET NetworkIdentity / NetworkManager components whose
/// scripts no longer exist). Menu: Tools > Cleanup.
/// Only removes components that are ALREADY broken — it cannot touch
/// working components.
/// </summary>
public static class MissingScriptCleanup
{
    [MenuItem("Tools/Cleanup/Remove Missing Scripts In Open Scene(s)")]
    public static void CleanOpenScenes()
    {
        int total = 0;
        for (int s = 0; s < SceneManager.sceneCount; s++)
        {
            Scene scene = SceneManager.GetSceneAt(s);
            if (!scene.isLoaded) continue;

            foreach (GameObject root in scene.GetRootGameObjects())
            {
                total += CleanHierarchy(root);
            }
            EditorSceneManager.MarkSceneDirty(scene);
        }
        Debug.Log($"[Cleanup] Removed {total} missing-script component(s) from open scene(s). Save the scene(s) to keep the changes.");
    }

    [MenuItem("Tools/Cleanup/Remove Missing Scripts In Selection")]
    public static void CleanSelection()
    {
        int total = 0;
        foreach (GameObject go in Selection.gameObjects)
        {
            total += CleanHierarchy(go);
        }
        Debug.Log($"[Cleanup] Removed {total} missing-script component(s) from the current selection.");
    }

    private static int CleanHierarchy(GameObject go)
    {
        int removed = GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
        foreach (Transform child in go.transform)
        {
            removed += CleanHierarchy(child.gameObject);
        }
        return removed;
    }
}
