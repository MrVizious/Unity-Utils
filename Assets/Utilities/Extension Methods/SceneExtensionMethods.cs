using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExtensionMethods
{

    public static class SceneExtensionMethods
    {
        public static T FindObjectOfType<T>(this Scene scene, bool searchInactive = false) where T : Object
        {
            if (!scene.isLoaded)
            {
                Debug.LogError("Scene is not loaded.");
                return null;
            }

            // Get all root game objects in the scene
            GameObject[] rootObjects = scene.GetRootGameObjects();

            // Loop through each root object and search for the component
            foreach (GameObject rootObject in rootObjects)
            {
                T component = rootObject.GetComponentInChildren<T>(searchInactive); // search inactive objects depending on input
                if (component != null)
                {
                    return component;
                }
            }

            // If not found, return null
            return null;
        }
    }

}