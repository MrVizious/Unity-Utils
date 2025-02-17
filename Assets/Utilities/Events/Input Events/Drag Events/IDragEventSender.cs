using System.Collections;
using System.Collections.Generic;
using DesignPatterns.EventBus;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InputEvents
{
    public interface IDragEventSender : IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [Button]
        void IInitializePotentialDragHandler.OnInitializePotentialDrag(PointerEventData eventData)
        {
            InitializePotentialDragEvent initializePotentialDragEvent = new InitializePotentialDragEvent(this, eventData);
            EventBus<InitializePotentialDragEvent>.Raise(initializePotentialDragEvent);
        }

        [Button]
        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            BeginDragEvent beginDragEvent = new BeginDragEvent(this, eventData);
            EventBus<BeginDragEvent>.Raise(beginDragEvent);
        }

        [Button]
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            DragEvent dragEvent = new DragEvent(this, eventData);
            EventBus<DragEvent>.Raise(dragEvent);
        }

        [Button]
        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            EndDragEvent endDragEvent = new EndDragEvent(this, eventData);
            EventBus<EndDragEvent>.Raise(endDragEvent);
        }


    }

}