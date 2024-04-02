using System;
using System.Collections.Generic;
using System.Linq;
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

        public static async UniTask ExecuteInBatches(IEnumerable<UniTask> taskList, int batchSize)
        {
            if (batchSize <= 0)
            {
                await UniTask.WhenAll(taskList);
                return;
            }
            List<UniTask> tempTaskList = new List<UniTask>();
            foreach (UniTask task in taskList)
            {
                tempTaskList.Add(task);
                if (tempTaskList.Count == batchSize)
                {
                    await UniTask.WhenAll(tempTaskList);
                    tempTaskList.Clear();
                }
            }
            await UniTask.WhenAll(tempTaskList);
            tempTaskList.Clear();
        }
    }
}
