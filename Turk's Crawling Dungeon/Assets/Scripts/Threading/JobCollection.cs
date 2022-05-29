using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Jobs;
using UnityEngine;

namespace TCD.Threading
{
    public class JobCollection 
    {
        private List<JobHandle> jobs = new List<JobHandle>();

        public void WaitFor()
        {
            if (jobs.Count == 0)
                return;
            foreach (JobHandle job in jobs)
            {
                if (!job.IsCompleted)
                    job.Complete();
            }
            jobs.Clear();
        }

        public void Add(JobHandle job) => jobs.Add(job);
    }
}
