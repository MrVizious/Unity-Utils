using UnityEngine;

namespace DesignPatterns
{
    /// <summary>
    /// MONOBEHAVIOR PSEUDO SINGLETON ABSTRACT CLASS WITH DEFAULT INSTANCE IN RESOURCES FOLDER
    /// usage	: Create a prefab at the route named the same as the class name that
    ///           has the class as a component
    /// example	: '''public class MyClass : SingletonDefaultInstance<MyClass> { ... }'''
    /// </summary>
    public abstract class SingletonDefaultInstance<T> : Singleton<T> where T : SingletonDefaultInstance<T>
    {
        [SerializeField]
        private bool _destroyOnLoad = true;
        protected override bool dontDestroyOnLoad
        {
            get => !_destroyOnLoad;
        }


        // if you want a child class to be use a different path to the prefab, change it
        // adding this section to the code overriding the path:
        // protected static new string resourcePath
        // {
        //     get => "Singleton Instances/" + typeof(T).Name;
        // }
        protected static string resourcesPath
        {
            get => "Singleton Instances/" + typeof(T).Name;
        }
        public static new T Instance
        {
            get
            {
                // If there is not an existing singleton instance
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));
                    if (_instance == null)
                    {
                        Debug.Log(resourcesPath);
                        _instance = Instantiate(
                            Resources.Load(resourcesPath)
                        ) as T;
                    }
                }
                return _instance;
            }
        }

    }
}