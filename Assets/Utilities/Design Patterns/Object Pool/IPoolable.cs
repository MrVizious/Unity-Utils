using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

namespace DesignPatterns
{
    public interface IPoolable
    {
        void OnPoolGet() { }
        void OnPoolRelease() { }
        void OnPoolDestroy() { }
        UltEvent onRelease { get; set; }
        void Release()
        {
            onRelease.Invoke();
        }
    }
}