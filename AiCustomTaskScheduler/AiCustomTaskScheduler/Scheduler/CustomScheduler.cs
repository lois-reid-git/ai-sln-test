using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AiCustomTaskScheduler.Scheduler
{
    /// <summary>
    /// This class implements the custom scheduling algorithm. 
    /// Tasks can be scheduled to start, run immediately, run on an interval or stopped.
    /// </summary>
    public class CustomScheduler
    {
        private static CustomScheduler instance;
        // If instance is null, then create
        public static CustomScheduler Instance => instance ?? (instance = new CustomScheduler());

        // Use the System.Threading timers
        private List<Timer> taskTimers = new List<Timer>();

        // Multiple threads could be attempting to update/access this dictionary
        private ConcurrentDictionary<string, Task> runningTasks = new ConcurrentDictionary<string, Task>();

        /// <summary>
        /// Schedules an action to run once at a given time UTC in the future. 
        /// If the scheduled date has already passed, then task is not run and info logged to user.
        /// </summary>
        public void ScheduleTask(DateTime utcScheduledTime, string taskName, Action action)
        {

        }

        /// <summary>
        /// Schedules a task to run immediately, and at every given interval (in seconds) thereafter.
        /// </summary>
        public void ScheduleRepeatingTask(int intervalSeconds, string taskName, Action action)
        { 

        }

        /// <summary>
        /// Schedules a one-off run of a task to start immediately.
        /// </summary>
        public void ScheduleImmediateTask(string taskName, Action action)
        {

        }

        /// <summary>
        /// Stops a task from running (if currently in a running state) and stops it from running again in the future.
        /// </summary>
        /// <returns></returns>
        public bool StopTask(string taskName)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskName"></param>
        /// <returns></returns>
        public bool StartTask(string taskName)
        {
            return false;
        }

        /// <summary>
        /// Checks if task is already running or is scheduled to run (by inbuilt C# scheduler) and manages
        /// the runningTasks dictionary. 
        /// </summary>
        private void InvokeTask(string taskName, Action action)
        {
            Task task = this.runningTasks.GetOrAdd(taskName, new Task(action));
        }

    }
}
