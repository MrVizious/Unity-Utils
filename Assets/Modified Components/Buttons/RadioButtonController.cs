using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioButtonController : MonoBehaviour
{
    private List<OnOffExtendedButton> buttons = new List<OnOffExtendedButton>();

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
        buttons.Add(button);
        button.onClick += () => OnClicked(button);
    }
    public void Unsubscribe(OnOffExtendedButton button)
    {
        buttons.Remove(button);
        button.onClick -= () => OnClicked(button);
    }
}
