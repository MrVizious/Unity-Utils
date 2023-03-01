using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace UltEvents
{

    public class ButtonEvent : MonoBehaviour
    {
        public string Description;
        public UltEvent actionsToExecute;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(UltEvents.ButtonEvent))]
public class ButtonEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myScript = target as UltEvents.ButtonEvent;
        base.OnInspectorGUI();
        if (GUILayout.Button("Execute"))
        {
            myScript.actionsToExecute.Invoke();
        }
    }
}
#endif