using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UltEvents;

namespace UtilityEvents
{

    public class ApplicationLifetimeCycleEvents : DesignPatterns.Singleton<ApplicationLifetimeCycleEvents>
    {
        public UltEvent onApplicationQuitEvent = new UltEvent();
        public UltEvent onApplicationPauseEvent = new UltEvent();
        public UltEvent onApplicationFocusEvent = new UltEvent();
        private new void OnApplicationQuit()
        {
            onApplicationQuitEvent.Invoke();
            base.OnApplicationQuit();
        }
        private void OnApplicationPause()
        {
            onApplicationPauseEvent.Invoke();
        }
        private void OnApplicationFocus()
        {
            onApplicationFocusEvent.Invoke();
        }

    }

}