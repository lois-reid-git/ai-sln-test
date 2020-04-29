using AiCustomTaskScheduler.Config;
using AiCustomTaskScheduler.Utilities;
using AiCustomTaskScheduler.Work;
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
        public ConcurrentDictionary<string, Task> runningTasks = new ConcurrentDictionary<string, Task>();
        private ConcurrentDictionary<string, Task> configuredTasks = new ConcurrentDictionary<string, Task>();

        /// <summary>
        /// Based on the list of task configurations in the config file, configures the list
        /// of tasks associated with this scheduler but does not start them.
        /// </summary>
        /// <param name="configuredTasks"></param>
        public void ConfigureTasks(JConfig configuredTasks)
        {
            // First iteration configures and starts all
            foreach (JConfig.JTask jTask in configuredTasks.ConfiguredTasks)
            {
                // A scheduled task is either repeating or one-time at given time
                if (jTask.ScheduledTask)
                {
                    // Task runs on interval
                    if(jTask.TaskScheduledTime == "")
                    {
                        instance.ScheduleRepeatingTask(jTask.TaskExecutionInterval, jTask.TaskName, () => instance.ExecuteProcess(jTask.ExecutableLocation));
                    }
                    else
                    {
                        // TODO some handling for conversion error
                        DateTime utcScheduled = (DateTime.Parse(jTask.TaskScheduledTime)).ToUniversalTime();
                        instance.ScheduleTask(utcScheduled, jTask.TaskName, () => instance.ExecuteProcess(jTask.ExecutableLocation));
                    }
                }
                else
                {
                    instance.ScheduleImmediateTask(jTask.TaskName, () => instance.ExecuteProcess(jTask.ExecutableLocation));
                }

            }

        }

        /// <summary>
        /// Schedules an action to run once at a given time UTC in the future. 
        /// If the scheduled date has already passed, then task is not run and info logged to user.
        /// </summary>
        public void ScheduleTask(DateTime utcScheduledTime, string taskName, Action action)
        {
            Console.WriteLine(String.Format("{0} - Scheduling task '{1}' to run once at {2}...", DateTime.UtcNow.ToString(GlobalItems.Iso8601UtcFormat), taskName, utcScheduledTime.ToString(GlobalItems.Iso8601UtcFormat)));

            // Check how long until scheduled time            
            TimeSpan timeDelay = utcScheduledTime - DateTime.UtcNow;

            // May have missed start - don't schedule, log to console
            if (timeDelay <= TimeSpan.Zero)
            { 
                Console.WriteLine(String.Format("{0} - Missed scheduled start of {1}. Not running task.", DateTime.UtcNow.ToString(GlobalItems.Iso8601UtcFormat), taskName));
            }
            else
            {
                // Create timer that will execute task at specified time
                Timer timer = new Timer(x => this.InvokeTask(taskName, action), null, timeDelay, new TimeSpan(-1));
                taskTimers.Add(timer);
            }

        }

        /// <summary>
        /// Schedules a task to run immediately, and at every given interval (in seconds) thereafter.
        /// </summary>
        public void ScheduleRepeatingTask(int intervalSeconds, string taskName, Action action)
        {
            Console.WriteLine(String.Format("{0} - Scheduling task '{1}' to run immediately and then every {2} seconds...", DateTime.UtcNow.ToString(GlobalItems.Iso8601UtcFormat), taskName, Convert.ToString(intervalSeconds)));
            
            // Create timer that will execute task immediately and then every x seconds
            Timer timer = new Timer(x => this.InvokeTask(taskName, action), null, TimeSpan.Zero, TimeSpan.FromSeconds(intervalSeconds));
            taskTimers.Add(timer);
        }

        /// <summary>
        /// Schedules a one-off run of a task to start immediately.
        /// </summary>
        public void ScheduleImmediateTask(string taskName, Action action)
        {
            Console.WriteLine(String.Format("{0} - Scheduling task '{1}' to run immediately...", DateTime.UtcNow.ToString(GlobalItems.Iso8601UtcFormat), taskName));
            // Create timer that will execute task at asap
            Timer timer = new Timer(x => this.InvokeTask(taskName, action), null, TimeSpan.Zero, new TimeSpan(-1));
            taskTimers.Add(timer);
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
            try
            {
                task.Start();
                Console.WriteLine(String.Format("{0} - Task '{1}' started...", DateTime.UtcNow.ToString(GlobalItems.Iso8601UtcFormat), taskName));
            }
            catch (InvalidOperationException)
            {
                // If status not running, then dispose and remove
                if (task.Status != TaskStatus.Running)
                {
                    try
                    {
                        task.Dispose();
                    }
                    catch (InvalidOperationException)
                    {
                        // Task is not in a 'finished' state. Cancel it as it's about to run at an unexpected time.
                        Console.WriteLine(String.Format("{0} - Task '{1}' unexpected state when trigger requested!!", DateTime.UtcNow.ToString(GlobalItems.Iso8601UtcFormat), taskName));
                    }
                    finally
                    {
                        this.runningTasks.TryRemove(taskName, out Task removedTask);
                    }
                }
            }
        }

        /// <summary>
        /// Configures and triggers the work process wrapper.
        /// TODO - this doesn't belong here.
        /// </summary>
        /// <param name="executableLoc"></param>
        private void ExecuteProcess(string executableLoc)
        {
            WorkerProcess process = new WorkerProcess(executableLoc);
            process.TriggerProcess();

        }

    }
}
