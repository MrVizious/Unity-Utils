using System.Collections;
using System.Collections.Generic;
using DesignPatterns.EventBus;
using UnityEngine;

namespace InputEvents
{

    public class ClickListener : MonoBehaviour
    {
        private PointerEventListener pointerEventListener;
        private DragEventListener dragEventListener;
        private void OnEnable()
        {
            pointerEventListener = new PointerEventListener(pointerClickAction: OnClick);
        }

        private void OnClick(PointerClickEvent clickEvent)
        {
            Debug.Log($"ClickEvent listened at {this}", this);
            Debug.Log($"ClickEvent sent from {clickEvent.systemEventHandler}", (Object)clickEvent.systemEventHandler);
        }
    }

}