using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class OnOffExtendedButton : ExtendedButton
{
    [OnValueChanged("SetOffSprite")]
    public Sprite onSprite;
    [OnValueChanged("SetOffSprite")]
    public Color onColor = Color.white;
    [OnValueChanged("SetOffSprite")]
    public Sprite offSprite;
    [OnValueChanged("SetOffSprite")]
    public Color offColor = Color.white;
    protected override void Start()
    {
        onClick.DynamicCalls += SetOnSprite;
    }

    public void SetOnSprite()
    {
        image.sprite = onSprite;
        image.color = onColor;
    }
    public void SetOffSprite()
    {
        image.sprite = offSprite;
        image.color = offColor;
    }

    private void OnApplicationQuit()
    {
        onClick.DynamicCalls -= SetOnSprite;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(OnOffExtendedButton))]
public class OnOffExtendedButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        OnOffExtendedButton targetExtendedButton = (OnOffExtendedButton)target;

        base.OnInspectorGUI();
        //EditorGUILayout.PropertyField(this.serializedObject.FindProperty("onPointerEnter"), true);
        //EditorGUILayout.PropertyField(this.serializedObject.FindProperty("onPointerExit"), true);

    }
}
#endif