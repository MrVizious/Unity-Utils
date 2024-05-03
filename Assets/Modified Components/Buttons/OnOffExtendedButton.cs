using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UltEvents;


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
    public UltEvent onSetOn = new UltEvent();
    public UltEvent onSetOff = new UltEvent();
    public bool isOn = false;
    protected override void Start()
    {
        base.Start();
        SetState(isOn);
    }
    public void SetState(bool newValue)
    {
        if (newValue)
        {
            image.sprite = onSprite;
            image.color = onColor;
        }
        else
        {
            image.sprite = offSprite;
            image.color = offColor;
        }
        isOn = newValue;
        if (isOn)
        {
            onSetOn.Invoke();
        }
        else onSetOff.Invoke();
    }
    [Button]
    public void SetOn()
    {
        SetState(true);
    }
    [Button]
    public void SetOff()
    {
        SetState(false);
    }

    [Button]
    public void Toggle()
    {
        SetState(!isOn);
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