using UnityEngine;
using DesignPatterns;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PoolExample : MonoBehaviour
{
    Pool<PoolableExample> pool;

    private void Start()
    {
        pool = new Pool<PoolableExample>(
            3, 50
        );
    }

    public void Spawn()
    {
        pool.Get();
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(PoolExample))]
public class PoolExampleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myScript = target as PoolExample;
        base.OnInspectorGUI();
        if (GUILayout.Button("Spawn"))
        {
            myScript.Spawn();
        }
    }
}
#endif
