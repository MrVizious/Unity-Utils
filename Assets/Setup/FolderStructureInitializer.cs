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
            GitIgnoreCreation();
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

    private static void GitIgnoreCreation()
    {
        string[] currentPaths = System.IO.Directory.GetFiles(Application.dataPath, typeof(FolderStructureInitializer).ToString() + ".cs", System.IO.SearchOption.AllDirectories);
        if (currentPaths.Length <= 0) return;

        string gitignorePath = currentPaths[0].Replace(typeof(FolderStructureInitializer).ToString() + ".cs", ".gitignore");
        if (!System.IO.File.Exists(gitignorePath)) return;

        string destPath = Application.dataPath.Replace("Assets", "") + ".gitignore";
        if (System.IO.File.Exists(destPath)) return;
        System.IO.File.Copy(gitignorePath, destPath);
    }
}
#endif