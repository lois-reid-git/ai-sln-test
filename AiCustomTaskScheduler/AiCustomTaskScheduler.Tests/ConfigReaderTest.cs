using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AiCustomTaskScheduler.Config;

namespace AiCustomTaskScheduler.Tests
{
    [TestClass]
    public class ConfigReaderTest
    {
        private const string INVALID_EXTENSION_PATH = @"C:\Users\lois-\Documents\ai-solutions\git\ai-sln-test\AiCustomTaskScheduler\AiCustomTaskScheduler.Tests\resources\invalid-extension.txt";
        private const string INVALID_JSON_PATH = @"C:\Users\lois-\Documents\ai-solutions\git\ai-sln-test\AiCustomTaskScheduler\AiCustomTaskScheduler.Tests\resources\invalid-json.json";
        private const string VALID_JSON_PATH = @"C:\Users\lois-\Documents\ai-solutions\git\ai-sln-test\AiCustomTaskScheduler\AiCustomTaskScheduler.Tests\resources\valid-config.json";

        [TestMethod]
        public void Initialise_With_Valid_Json()
        {
            // Setup and Action
            ConfigReader reader = new ConfigReader(VALID_JSON_PATH);

            // Assertions
            StringAssert.Contains(reader.configPath, VALID_JSON_PATH);
            Assert.IsNull(reader.taskConfigurations);
        }

        [TestMethod]
        public void Initialise_With_Invalid_Json()
        {
            // Setup and Action
            try
            {
                ConfigReader reader = new ConfigReader(INVALID_JSON_PATH);
            }
            catch(ConfigReaderException e)
            {
                StringAssert.Contains(e.Message, "JSON");
                return;
            }

            Assert.Fail("Expected exception not thrown");
        }

        [TestMethod]
        public void Initialise_With_Invalid_Ext()
        {
            // Setup and Action
            try
            {
                ConfigReader reader = new ConfigReader(INVALID_EXTENSION_PATH);
            }
            catch (ConfigReaderException e)
            {
                StringAssert.Contains(e.Message, "JSON");
                return;
            }
            Assert.Fail("Expected exception not thrown");
        }

        [TestMethod]
        public void Initialise_Read_Json()
        {
            // Setup and Action
            ConfigReader reader = new ConfigReader(VALID_JSON_PATH);
            reader.ReadConfig();

            // Assertions
            Assert.IsNotNull(reader.taskConfigurations);
            Assert.IsTrue(reader.taskConfigurations.ConfiguredTasks.Count == 2);
        }
    }
}
