using System.Collections;
using System.Collections.Generic;
using DesignPatterns.EventBus;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InputEvents
{

    public interface IPointerEventSender : IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Button]
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            PointerClickEvent pointerClickEvent = new PointerClickEvent(this, eventData);
            EventBus<PointerClickEvent>.Raise(pointerClickEvent);
        }

        [Button]
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            PointerDownEvent pointerDownEvent = new PointerDownEvent(this, eventData);
            EventBus<PointerDownEvent>.Raise(pointerDownEvent);
        }

        [Button]
        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            PointerUpEvent pointerUpEvent = new PointerUpEvent(this, eventData);
            EventBus<PointerUpEvent>.Raise(pointerUpEvent);
        }

        [Button]
        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            PointerEnterEvent pointerEnterEvent = new PointerEnterEvent(this, eventData);
            EventBus<PointerEnterEvent>.Raise(pointerEnterEvent);
        }

        [Button]
        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            PointerExitEvent pointerExitEvent = new PointerExitEvent(this, eventData);
            EventBus<PointerExitEvent>.Raise(pointerExitEvent);
        }

    }

}