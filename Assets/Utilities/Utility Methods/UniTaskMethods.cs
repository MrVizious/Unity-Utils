using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public static async UniTask<T> ExecuteAndRaiseCancelIfValue<T>(UniTask<T> task, CancellationTokenSource cancellationToken, T valueToCancelOn)
        {
            var result = await task;
            if (EqualityComparer<T>.Default.Equals(result, valueToCancelOn))
            {
                cancellationToken.Cancel();
                throw new OperationCanceledException(cancellationToken.Token);
            }
            return result;
        }

        public static async UniTask WhenAllUnlessValue<T>(IEnumerable<UniTask<T>> tasks, T valueToCancelOn)
        {
            var cts = new CancellationTokenSource();

            foreach (UniTask<T> task in tasks)
            {
                await ExecuteAndRaiseCancelIfValue<T>(task, cts, valueToCancelOn);
            }

            try
            {
                await UniTask.WhenAll(tasks);
                // If we reach here, both tasks completed successfully without null results
            }
            catch (OperationCanceledException e)
            {
                throw e;
            }
        }

        public static UniTask<TResult> WrapTaskWithCast<TOriginal, TResult>(UniTask<TOriginal> task) where TOriginal : TResult
        {
            return task.ContinueWith(t => (TResult)t);
        }
    }
}
