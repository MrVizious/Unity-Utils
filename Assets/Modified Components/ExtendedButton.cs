using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExtendedButton : Button
{
    public UnityEvent onPointerEnter = new UnityEvent();
    public UnityEvent onPointerExit = new UnityEvent();

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        onPointerEnter.Invoke();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        onPointerExit.Invoke();
    }

}


#if UNITY_EDITOR
[CustomEditor(typeof(ExtendedButton))]
public class ExtendedButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ExtendedButton targetExtendedButton = (ExtendedButton)target;

        base.OnInspectorGUI();
        //EditorGUILayout.PropertyField(this.serializedObject.FindProperty("onPointerEnter"), true);
        //EditorGUILayout.PropertyField(this.serializedObject.FindProperty("onPointerExit"), true);

    }
}
#endif