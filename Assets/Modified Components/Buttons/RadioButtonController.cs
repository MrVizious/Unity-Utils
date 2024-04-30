using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioButtonController : MonoBehaviour
{
    private OnOffExtendedButton[] buttons;

    private void Start()
    {
        buttons = GetComponentsInChildren<OnOffExtendedButton>();
        foreach (OnOffExtendedButton button in buttons)
        {
            button.onClick += () => OnClicked(button);
        }
    }

    private void OnClicked(OnOffExtendedButton button)
    {
        foreach (OnOffExtendedButton otherButton in buttons)
        {
            if (otherButton == button) button.SetOnSprite();
            else otherButton.SetOffSprite();
        }
    }
}
