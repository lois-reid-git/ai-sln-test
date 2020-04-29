using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiCustomTaskScheduler.Config
{
    /// <summary>
    /// Class representing the JSON configuration file. This does not negate the need of a JSON Schema class.
    /// </summary>
    [Serializable]
    public class JConfig
    {
        [JsonProperty("task_config")]
        public List<JTask> ConfiguredTasks { get; set; }

        [Serializable]
        public class JTask
        {
            [JsonProperty("task_name")]
            public string TaskName { get; set; }

            [JsonProperty("task_location")]
            public string ExecutableLocation { get; set; }

            [JsonProperty("task_interval")]
            public int TaskExecutionInterval { get; set; }

            [JsonProperty("task_scheduled_time")]
            public string TaskScheduledTime { get; set;}

            [JsonProperty("task_scheduled")]
            public bool ScheduledTask { get; set; }

        }
    }
}
