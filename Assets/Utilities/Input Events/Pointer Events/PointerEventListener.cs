using System.Collections;
using System.Collections.Generic;
using DesignPatterns.EventBus;
using UnityEngine;

namespace InputEvents
{
    public class PointerEventListener
    {
        EventBinding<PointerClickEvent> pointerClickEventBinding { get; set; }
        EventBinding<PointerDownEvent> pointerDownEventBinding { get; set; }
        EventBinding<PointerUpEvent> pointerUpEventBinding { get; set; }
        EventBinding<PointerEnterEvent> pointerEnterEventBinding { get; set; }
        EventBinding<PointerExitEvent> pointerExitEventBinding { get; set; }

        public PointerEventListener(System.Action<PointerClickEvent> pointerClickAction = null,
                                    System.Action<PointerDownEvent> pointerDownAction = null,
                                    System.Action<PointerUpEvent> pointerUpAction = null,
                                    System.Action<PointerEnterEvent> pointerEnterAction = null,
                                    System.Action<PointerExitEvent> pointerExitAction = null)
        {
            if (pointerClickAction != null)
            {
                RegisterPointerClickEvent(pointerClickAction);
            }
            if (pointerDownAction != null)
            {
                RegisterPointerDownEvent(pointerDownAction);
            }
            if (pointerUpAction != null)
            {
                RegisterPointerUpEvent(pointerUpAction);
            }
            if (pointerEnterAction != null)
            {
                RegisterPointerEnterEvent(pointerEnterAction);
            }
            if (pointerExitAction != null)
            {
                RegisterPointerExitEvent(pointerExitAction);
            }
        }
        public void RegisterPointerClickEvent(System.Action<PointerClickEvent> action)
        {
            pointerClickEventBinding = new EventBinding<PointerClickEvent>(action);
            EventBus<PointerClickEvent>.Register(pointerClickEventBinding);
        }

        public void RegisterPointerDownEvent(System.Action<PointerDownEvent> action)
        {
            pointerDownEventBinding = new EventBinding<PointerDownEvent>(action);
            EventBus<PointerDownEvent>.Register(pointerDownEventBinding);
        }

        public void RegisterPointerUpEvent(System.Action<PointerUpEvent> action)
        {
            pointerUpEventBinding = new EventBinding<PointerUpEvent>(action);
            EventBus<PointerUpEvent>.Register(pointerUpEventBinding);
        }

        public void RegisterPointerEnterEvent(System.Action<PointerEnterEvent> action)
        {
            pointerEnterEventBinding = new EventBinding<PointerEnterEvent>(action);
            EventBus<PointerEnterEvent>.Register(pointerEnterEventBinding);
        }

        public void RegisterPointerExitEvent(System.Action<PointerExitEvent> action)
        {
            pointerExitEventBinding = new EventBinding<PointerExitEvent>(action);
            EventBus<PointerExitEvent>.Register(pointerExitEventBinding);
        }

    }

}