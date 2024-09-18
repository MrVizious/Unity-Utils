using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UtilityMethods
{

    public class UniTaskPool
    {
        public int maxNumberOfConcurrentTasks = 10;
        private int numberOfExecutingTasks = 0;
        private Queue<UniTask> tasksInQueue = new Queue<UniTask>();
        public UniTaskPool() { }
        public UniTaskPool(int maxNumberOfConcurrentTasks)
        {
            this.maxNumberOfConcurrentTasks = maxNumberOfConcurrentTasks;
        }

        public void Enqueue(UniTask task)
        {
            tasksInQueue.Enqueue(task);
        }
        public void Run()
        {
            for (int i = numberOfExecutingTasks; i <= maxNumberOfConcurrentTasks; i++)
            {
                if (tasksInQueue.TryDequeue(out UniTask task))
                {
                    Run(task);
                }
            }
        }
        public void Run(UniTask task)
        {
            if (numberOfExecutingTasks <= maxNumberOfConcurrentTasks)
            {
                numberOfExecutingTasks++;
                task.ContinueWith(OnExecutionCompleted).Forget();
            }
            else
            {
                Enqueue(task);
            }
        }
        private void OnExecutionCompleted()
        {
            numberOfExecutingTasks--;
            RunNextTaskInQueue();
        }
        private void RunNextTaskInQueue()
        {
            if (tasksInQueue.TryDequeue(out UniTask task))
            {
                Run(task);
            }
        }
    }
    public static class UniTaskMethods
    {
        public static async UniTask DelayedFunction(Action action, float secondsToWait)
        {
            int millisToWait = (int)(secondsToWait * 1000);
            await UniTask.Delay(millisToWait);
            action.Invoke();
        }

        public static async UniTask ExecuteInBatches(IEnumerable<UniTask> taskList, int batchSize, bool yield = false, VariableProgress progress = null)
        {
            if (taskList == null || taskList.Count() == 0) return;
            if (batchSize <= 0)
            {
                if (progress == null)
                {
                    await UniTask.WhenAll(taskList);
                }
                else
                {
                    await WhenAllWithReporting(taskList, progress);
                }
                return;
            }
            List<UniTask> tempTaskList = new List<UniTask>();
            foreach (UniTask task in taskList)
            {
                tempTaskList.Add(task);
                if (tempTaskList.Count == batchSize)
                {
                    if (progress == null)
                    {
                        await UniTask.WhenAll(tempTaskList);
                    }
                    else
                    {
                        await WhenAllWithReporting(tempTaskList, progress);
                    }
                    if (yield) await UniTask.Yield();
                    tempTaskList.Clear();
                }
            }
            if (tempTaskList.Count <= 0) return;
            if (progress == null)
            {
                await UniTask.WhenAll(tempTaskList);
            }
            else
            {
                await WhenAllWithReporting(tempTaskList, progress);
            }
            if (yield) await UniTask.Yield();
            tempTaskList.Clear();
        }

        public static async UniTask WhenAllWithReporting(IEnumerable<UniTask> tasks, VariableProgress progress)
        {
            HashSet<UniTask> tasksToWaitFor = new HashSet<UniTask>();
            foreach (UniTask task in tasks)
            {
                tasksToWaitFor.Add(ExecuteWithReporting(task, progress));
            }
            await UniTask.WhenAll(tasksToWaitFor);
        }

        public static async UniTask ExecuteWithReporting(UniTask task, VariableProgress progress, bool addToTotal = true, bool addToCompleted = true)
        {
            if (addToTotal) progress?.AddToTotal();
            await task;
            if (addToCompleted) progress?.AddToCompleted();
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

        /// <summary>
        /// Executes a series of tasks, and if one of them returns the given value valueToCancelOn,
        /// cancels any remaining tasks
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tasks"></param>
        /// <param name="valueToCancelOn"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns a UniTask of the second type. Example: UniTask<Cat> -> UniTask<Animal>
        /// </summary>
        /// <typeparam name="TOriginal"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="task"></param>
        /// <returns></returns>
        public static UniTask<TResult> WrapTaskWithCast<TOriginal, TResult>(UniTask<TOriginal> task) where TOriginal : TResult
        {
            return task.ContinueWith(t => (TResult)t);
        }
    }
}
