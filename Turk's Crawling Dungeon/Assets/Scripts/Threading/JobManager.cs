using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;
using Unity.Jobs;

namespace TCD.Threading
{
    public class JobManager : MonoBehaviour
    {
        private static Dictionary<Guid, JobHandle> jobHandles = new Dictionary<Guid, JobHandle>();
        private static Dictionary<JobGroup, JobCollection> collections = new Dictionary<JobGroup, JobCollection>();
        private static BlockingCollection<IDisposableJob> inputDisposals = new BlockingCollection<IDisposableJob>();

        private List<IDisposableJob> incompleteJobs = new List<IDisposableJob>();

        private void Update()
        {
            if (!inputDisposals.IsAddingCompleted)
                return;
            for (int i = inputDisposals.Count - 1; i >= 0; i--)
            {
                var job = inputDisposals.Take();

                if (jobHandles[job.Guid].IsCompleted)
                {
                    job.Dispose();
                    jobHandles.Remove(job.Guid);
                }
                else
                    incompleteJobs.Add(job);
            }
            foreach (var job in incompleteJobs)
               inputDisposals.Add(job);
            incompleteJobs.Clear();
        }

        public static void Dispose(IDisposableJob job) => inputDisposals.Add(job);

        public static void EnqueueJob<T>(T job, JobGroup group = JobGroup.None) where T : struct, IDisposableJob
        {
            //DebugLogger.Log("[Threading] - Enqueueing action...");
            var handle = job.Schedule();
            jobHandles[job.Guid] = handle;
            GetThreadCollection(group).Add(handle);
        }

        private static JobCollection GetThreadCollection(JobGroup group)
        {
            if (!collections.TryGetValue(group, out var collection))
            {
                collection = new JobCollection();
                collections[group] = collection;
            }
            return collection;
        }

        public static void WaitFor(JobGroup group)
        {
            //DebugLogger.Log($"[Threading] - Waiting for {group}.");
            GetThreadCollection(group).WaitFor();
            //DebugLogger.Log($"[Threading] - {group} finished.");
        }
    }
}
