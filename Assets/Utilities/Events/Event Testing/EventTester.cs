// Created by: Javier Riera (https://mrvizious.github.io)
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class EventTester : MonoBehaviour
{
    #region Public Variables
    public TMP_Text displayText;
    [SerializeField, Tooltip("The text you want to show when the event is received")]
    private string displayMessage = "Event was invoked";
    // public string _displayMessage = "Event was invoked";
    #endregion

    #region Private Variables
    #endregion

    [Button]
    private void LogEvent()
    {
        Debug.Log(displayMessage, this);
        if (displayText == null) return;
        displayText.text = displayMessage;
    }
}

public class EventTester<T> : EventTester
{
    [SerializeField, Tooltip("The text you want to show when the event is received. Default is \"Event of type {TYPE} was invoked with value {VALUE}\"")]
    private string typedDisplayMessage = "Event of type {TYPE} was invoked with value {VALUE}";

    [Button]
    public void LogEventValue(T value)
    {
        string replacedDisplayMessage = typedDisplayMessage.Replace("{TYPE}", typeof(T).ToString()).Replace("{VALUE}", value.ToString());
        Debug.Log(replacedDisplayMessage, this);
        if (displayText == null) return;
        displayText.text = replacedDisplayMessage;
    }
}