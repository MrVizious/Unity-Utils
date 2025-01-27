using System.Collections;
using System.Collections.Generic;
using DesignPatterns.EventBus;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InputEvents
{

    public interface IPointerEvent : IEvent
    {
        public IEventSystemHandler systemEventHandler { get; set; }
        public PointerEventData pointerEventData { get; set; }

    }

    public struct PointerClickEvent : IPointerEvent
    {
        public IEventSystemHandler systemEventHandler { get; set; }
        public PointerEventData pointerEventData { get; set; }

        public PointerClickEvent(IPointerEventSender systemEventHandler, PointerEventData pointerEventData)
        {
            this.pointerEventData = pointerEventData;
            this.systemEventHandler = systemEventHandler;
        }
    }

    public struct PointerDownEvent : IPointerEvent
    {
        public IEventSystemHandler systemEventHandler { get; set; }
        public PointerEventData pointerEventData { get; set; }

        public PointerDownEvent(IPointerEventSender systemEventHandler, PointerEventData pointerEventData)
        {
            this.pointerEventData = pointerEventData;
            this.systemEventHandler = systemEventHandler;
        }
    }

    public struct PointerUpEvent : IPointerEvent
    {
        public IEventSystemHandler systemEventHandler { get; set; }
        public PointerEventData pointerEventData { get; set; }

        public PointerUpEvent(IPointerEventSender systemEventHandler, PointerEventData pointerEventData)
        {
            this.pointerEventData = pointerEventData;
            this.systemEventHandler = systemEventHandler;
        }
    }
    public struct PointerEnterEvent : IPointerEvent
    {
        public IEventSystemHandler systemEventHandler { get; set; }
        public PointerEventData pointerEventData { get; set; }

        public PointerEnterEvent(IPointerEventSender systemEventHandler, PointerEventData pointerEventData)
        {
            this.pointerEventData = pointerEventData;
            this.systemEventHandler = systemEventHandler;
        }
    }


    public struct PointerExitEvent : IPointerEvent
    {
        public IEventSystemHandler systemEventHandler { get; set; }
        public PointerEventData pointerEventData { get; set; }

        public PointerExitEvent(IPointerEventSender systemEventHandler, PointerEventData pointerEventData)
        {
            this.pointerEventData = pointerEventData;
            this.systemEventHandler = systemEventHandler;
        }
    }
}