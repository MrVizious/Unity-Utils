using System;
using Cysharp.Threading.Tasks;

namespace UtilityMethods
{
    public static class UniTaskMethods
    {
        public static async UniTask DelayedFunction(Action action, float secondsToWait)
        {
            int millisToWait = (int)(secondsToWait * 1000);
            await UniTask.Delay(millisToWait);
            action.Invoke();
        }
    }
}
