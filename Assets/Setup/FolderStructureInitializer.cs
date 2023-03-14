#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class FolderStructureInitializer
{
    static FolderStructureInitializer()
    {
        if (!SessionState.GetBool("FirstInitDone", false))
        {
            FolderCreation();
            SessionState.SetBool("FirstInitDone", true);
        }
    }

    private static void FolderCreation()
    {
        if (AssetDatabase.IsValidFolder("Assets/_Home_")) return;
        AssetDatabase.CreateFolder("Assets", "_Home_");
        AssetDatabase.CreateFolder("Assets/_Home_", "Art");
        AssetDatabase.CreateFolder("Assets/_Home_", "Events");
        AssetDatabase.CreateFolder("Assets/_Home_", "Scenes");
        AssetDatabase.CreateFolder("Assets/_Home_", "Scriptable Object Instances");
        AssetDatabase.CreateFolder("Assets/_Home_", "Scripts");
    }
}
#endif