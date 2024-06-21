using System.Collections;
using System.Collections.Generic;
using DesignPatterns.EventBus;
using UnityEngine;

namespace InputEvents
{

    public class DragEventListener
    {
        EventBinding<InitializePotentialDragEvent> initializePotentialDragEventBinding { get; set; }
        EventBinding<BeginDragEvent> beginDragEventBinding { get; set; }
        EventBinding<DragEvent> dragEventBinding { get; set; }
        EventBinding<EndDragEvent> endDragEventBinding { get; set; }

        public DragEventListener(System.Action initializePotentialDragAction = null,
                                 System.Action beginDragAction = null,
                                 System.Action dragAction = null,
                                 System.Action endDragEventAction = null)
        {
            if (initializePotentialDragAction != null)
            {
                RegisterInitializePotentialDragEvent(initializePotentialDragAction);
            }
            if (beginDragAction != null)
            {
                RegisterBeginDragEvent(beginDragAction);
            }
            if (dragAction != null)
            {
                RegisterDragEvent(dragAction);
            }
            if (endDragEventAction != null)
            {
                RegisterEndDragEvent(endDragEventAction);
            }
        }
        public void RegisterInitializePotentialDragEvent(System.Action action)
        {
            initializePotentialDragEventBinding = new EventBinding<InitializePotentialDragEvent>(action);
            EventBus<InitializePotentialDragEvent>.Register(initializePotentialDragEventBinding);
        }

        public void RegisterBeginDragEvent(System.Action action)
        {
            beginDragEventBinding = new EventBinding<BeginDragEvent>(action);
            EventBus<BeginDragEvent>.Register(beginDragEventBinding);
        }
        public void RegisterDragEvent(System.Action action)
        {
            dragEventBinding = new EventBinding<DragEvent>(action);
            EventBus<DragEvent>.Register(dragEventBinding);
        }
        public void RegisterEndDragEvent(System.Action action)
        {
            endDragEventBinding = new EventBinding<EndDragEvent>(action);
            EventBus<EndDragEvent>.Register(endDragEventBinding);
        }

    }

}