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
    [SerializeField]
    private Sprite _onSprite = null;
    public Sprite onSprite
    {
        get { return _onSprite; }
        set
        {
            _onSprite = value;
            SetState(isOn);
        }
    }

    [SerializeField]
    private Color _onColor = Color.white;
    public Color onColor
    {
        get { return _onColor; }
        set
        {
            _onColor = value;
            SetState(isOn);
        }
    }
    [SerializeField]
    private Sprite _offSprite;
    public Sprite offSprite
    {
        get { return _offSprite; }
        set
        {
            _offSprite = value;
            SetState(isOn);
        }
    }
    [SerializeField]
    private Color _offColor = Color.white;
    public Color offColor
    {
        get { return _offColor; }
        set
        {
            _offColor = value;
            SetState(isOn);
        }
    }
    public UltEvent onSetOn = new UltEvent();
    public UltEvent onSetOff = new UltEvent();
    public bool startOn = false;
    public bool isOn { get; private set; }
    protected override void OnEnable()
    {
        if (!Application.isPlaying) return;
        base.OnEnable();
        GetComponentInParent<RadioButtonController>()?.Subscribe(this);
        if (!Application.isPlaying) return;
        SetState(startOn);
    }
    protected override void OnDisable()
    {
        if (!Application.isPlaying) return;
        GetComponentInParent<RadioButtonController>()?.Unsubscribe(this);
    }
    [Button]
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