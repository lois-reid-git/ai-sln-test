using AiCustomTaskScheduler.Config;
using AiCustomTaskScheduler.Scheduler;
using AiCustomTaskScheduler.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiCustomTaskScheduler
{
    class Program
    {

        static void Main(string[] args)
        {
            string configFile = "";
            // First command-line argument is the location of the config file
            try
            {
                configFile = args[0];
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Command-line argument of configuration file full path must be specified.");
                AnyKeyExit();
            }

            // Read in task config
            Console.WriteLine(String.Format("{0} - Reading task config from {1}...", DateTime.UtcNow.ToString(GlobalItems.Iso8601UtcFormat), configFile));
            try
            {
                ConfigReader configReader = new ConfigReader(configFile);
                configReader.ReadConfig();
                Console.WriteLine(String.Format("{0} - Task config loaded.", DateTime.UtcNow.ToString(GlobalItems.Iso8601UtcFormat)));

                // Load tasks to scheduler and start (first-iteration of scheduler work)
                Console.WriteLine(String.Format("{0} - Scheduling all {1} tasks.", DateTime.UtcNow.ToString(GlobalItems.Iso8601UtcFormat), Convert.ToString(configReader.taskConfigurations.ConfiguredTasks.Count)));
                try
                {
                    CustomScheduler.Instance.ConfigureTasks(configReader.taskConfigurations);
                }
                catch(Exception e)
                {
                    Console.WriteLine(String.Format("{0} - Exception caught during task scheduling: {1}...", DateTime.UtcNow.ToString(GlobalItems.Iso8601UtcFormat), e.Message));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(String.Format("{0} - Exception caught during config load: {1}...", DateTime.UtcNow.ToString(GlobalItems.Iso8601UtcFormat), e.Message));
                AnyKeyExit();
            }

            Console.ReadKey();

        }

        /// <summary>
        /// Method to stop main program with any key.
        /// </summary>
        private static void AnyKeyExit()
        {
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(1);
        }

    }
}
