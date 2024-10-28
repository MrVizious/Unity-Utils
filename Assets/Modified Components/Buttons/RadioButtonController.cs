using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class RadioButtonController : SerializedMonoBehaviour
{
    [OdinSerialize]
    private HashSet<OnOffExtendedButton> buttons = new HashSet<OnOffExtendedButton>();

    private void OnClicked(OnOffExtendedButton button)
    {
        foreach (OnOffExtendedButton otherButton in buttons)
        {
            if (otherButton == button) button.SetOn();
            else otherButton.SetOff();
        }
    }

    public void Subscribe(OnOffExtendedButton button)
    {
        Debug.Log($"Subcribing button {button.gameObject}", this);
        if (buttons.Add(button))
            button.onClick += () => OnClicked(button);
    }
    public void Unsubscribe(OnOffExtendedButton button)
    {
        if (buttons.Remove(button))
            button.onClick -= () => OnClicked(button);
    }

    private void OnDestroy()
    {
        buttons.Clear();
    }
}
