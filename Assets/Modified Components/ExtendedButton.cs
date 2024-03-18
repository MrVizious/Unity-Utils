using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UltEvents;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExtendedButton : Button
{
    public new UltEvent onClick = new UltEvent();
    public UltEvent onPointerEnter = new UltEvent();
    public UltEvent onPointerExit = new UltEvent();
    public UltEvent onButtonDown = new UltEvent();
    public UltEvent onButtonUp = new UltEvent();

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        onClick.Invoke();
    }
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
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        onButtonDown.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        onButtonUp.Invoke();
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