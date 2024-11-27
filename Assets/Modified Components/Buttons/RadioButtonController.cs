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
        if (buttons.Add(button))
        {
            button.onClick += () => OnClicked(button);
        }
    }
    public void Unsubscribe(OnOffExtendedButton button)
    {
        button.onClick -= () => OnClicked(button);
    }

    private void OnDestroy()
    {
        foreach (OnOffExtendedButton button in buttons)
        {
            Unsubscribe(button);
        }
        buttons.Clear();
    }
}
