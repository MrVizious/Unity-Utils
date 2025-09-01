using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DesignPatterns
{
    /// <summary>
    /// MONOBEHAVIOR PSEUDO SINGLETON ABSTRACT CLASS
    /// usage	: best is to be attached to a gameobject but if not that is ok,
    /// 		: this will create one on first access
    /// example	: '''public sealed class MyClass : Singleton<MyClass> { ... }'''
    /// references	: http://tinyurl.com/cc73a9h
    /// </summary>
    public abstract class Singleton<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour
    {
        protected static T _instance = null;
        /// <summary>
        /// This is the cached instance, and has no assurance of not being null
        /// </summary>
        public static T cachedInstance => _instance;


        // if you want a child class to be destroyed on load, copy this code into it:
        // protected override bool dontDestroyOnLoad
        // {
        //     get { return false; }
        // }
        protected virtual bool dontDestroyOnLoad
        {
            get { return true; }
        }

        /// <summary>
        /// This variable controls whether a new instance found in a newly loaded scene should overwrite the existing
        /// instance or not
        /// </summary>
        // if you want a child class to keep the newest instance
        // protected override bool keepOldestInstance
        // {
        //     get { return false; }
        // }
        protected virtual bool keepOldestInstance
        {
            get { return true; }
        }

        public static bool IsAwake { get { return (_instance != null); } }

        /// <summary>
        /// gets the instance of this Singleton
        /// use this for all instance calls:
        /// MyClass.Instance.MyMethod();
        /// or make your public methods static
        /// and have them use Instance
        /// </summary>
        public static async UniTask<T> GetInstance()
        {
            // If there is not an existing singleton instance
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();
                if (_instance == null)
                {
                    string goName = typeof(T).ToString();

                    GameObject go = GameObject.Find(goName);
                    if (go == null)
                    {
                        go = new GameObject();
                        go.name = goName;
                    }
                    _instance = go.AddComponent<T>();
                    await UniTask.WaitUntil(() => _instance != null);
                }
            }
            return _instance;
        }


        protected virtual void Awake()
        {
            // If an instance already exists and it's not this one
            if (_instance != null && _instance != this)
            {
                if (keepOldestInstance)
                {
                    // Keep the old instance, destroy this one
                    Destroy(this.gameObject);
                    return;
                }
                else
                {
                    // Replace the old instance with this one, destroy the old instance
                    Destroy(_instance.gameObject);
                    _instance = this as T;
                }
            }
            else if (_instance == null)
            {
                // Assign this instance if none exists
                _instance = this as T;
            }

            // Ensure this instance persists if specified
            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }


        /// <summary>
        /// for garbage collection
        /// </summary>
        public virtual void OnApplicationQuit()
        {
            // release reference on exit
            _instance = null;
        }

        // in your child class you can implement Awake()
        // and add any initialization code you want such as
        // DontDestroyOnLoad(go);
        // if you want this to persist across loads
        // or if you want to set a parent object with SetParent()

        /// <summary>
        /// parent this to another gameobject by string
        /// call from Awake if you so desire
        /// </summary>
        protected void SetParent(string parentGOName)
        {
            if (parentGOName != null)
            {
                GameObject parentGO = GameObject.Find(parentGOName);
                if (parentGO == null)
                {
                    parentGO = new GameObject();
                    parentGO.name = parentGOName;
                }
                this.transform.parent = parentGO.transform;
            }
        }
    }

}