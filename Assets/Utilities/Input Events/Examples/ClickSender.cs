using System.Collections;
using System.Collections.Generic;
using DesignPatterns.EventBus;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InputEvents
{

    public class ClickSender : MonoBehaviour, IPointerEventSender, IDragEventSender
    {
    }
}