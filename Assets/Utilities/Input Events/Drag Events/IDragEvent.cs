using System.Collections;
using System.Collections.Generic;
using DesignPatterns.EventBus;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InputEvents
{

    public interface IDragEvent : IEvent
    {
        public IEventSystemHandler systemEventHandler { get; set; }
        public PointerEventData dragEventData { get; set; }

    }

    // OnDrag Calls
    public struct DragEvent : IPointerEvent
    {
        public IEventSystemHandler systemEventHandler { get; set; }
        public PointerEventData pointerEventData { get; set; }

        public DragEvent(IDragEventSender systemEventHandler, PointerEventData pointerEventData)
        {
            this.pointerEventData = pointerEventData;
            this.systemEventHandler = systemEventHandler;
        }

    }

    public struct EndDragEvent : IPointerEvent
    {
        public IEventSystemHandler systemEventHandler { get; set; }
        public PointerEventData pointerEventData { get; set; }

        public EndDragEvent(IDragEventSender systemEventHandler, PointerEventData pointerEventData)
        {
            this.pointerEventData = pointerEventData;
            this.systemEventHandler = systemEventHandler;
        }
    }

    public struct BeginDragEvent : IPointerEvent
    {
        public IEventSystemHandler systemEventHandler { get; set; }
        public PointerEventData pointerEventData { get; set; }

        public BeginDragEvent(IDragEventSender systemEventHandler, PointerEventData pointerEventData)
        {
            this.pointerEventData = pointerEventData;
            this.systemEventHandler = systemEventHandler;
        }
    }

    public struct InitializePotentialDragEvent : IPointerEvent
    {
        public IEventSystemHandler systemEventHandler { get; set; }
        public PointerEventData pointerEventData { get; set; }

        public InitializePotentialDragEvent(IDragEventSender systemEventHandler, PointerEventData pointerEventData)
        {
            this.pointerEventData = pointerEventData;
            this.systemEventHandler = systemEventHandler;
        }
    }
}