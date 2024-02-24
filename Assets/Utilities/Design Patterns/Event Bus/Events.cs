using System;

namespace DesignPatterns.EventBus
{
    public interface IEvent { }

    #region ShirtCreationMinigame

    public struct ToolClickedEvent : IEvent
    {
        public ToolData toolData;
        public Type toolType;
    }

    #endregion
}