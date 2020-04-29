using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AiCustomTaskScheduler.Config;
using AiCustomTaskScheduler.Work;
using System.IO;

namespace AiCustomTaskScheduler.Tests
{
    [TestClass]
    public class WorkerProcessTest
    {
        private const string VALID_EXE = @"C:\Users\lois-\Documents\ai-solutions\git\ai-sln-test\AiCustomTaskScheduler\resources\worker-task\AiCustomTaskScheduler.Worker.exe";
        private const string NON_EXISTANT_EXE = @"C:\Users\lois-\Documents\ai-solutions\git\ai-sln-test\AiCustomTaskScheduler\AiCustomTaskScheduler.Tests\resources\AiCustomTaskScheduler.Worker.exe";
        private const string NON_EXE_EXE = @"C:\Users\lois-\Documents\ai-solutions\git\ai-sln-test\AiCustomTaskScheduler\AiCustomTaskScheduler.Tests\resources\AiCustomTaskScheduler.Worker.dll";

        [TestMethod]
        public void Initialize_With_Valid_Exe()
        {
            // Setup and Action
            WorkerProcess process = new WorkerProcess(VALID_EXE);

            // Assertions
            StringAssert.Contains(process.executablePath, VALID_EXE);
        }

        [TestMethod]
        public void Initialize_And_Trigger_Valid_Exe()
        {
            // Setup and Action
            WorkerProcess process = new WorkerProcess(VALID_EXE);
            process.TriggerProcess();

            // Assertions
            StringAssert.Contains(process.executablePath, VALID_EXE);
        }


        [TestMethod]
        public void Initialize_With_Nonexistant_Exe()
        {
            WorkerProcess process;
            try
            {
                // Setup and Action
                process = new WorkerProcess(NON_EXISTANT_EXE);
            }
            catch(FileNotFoundException)
            {
                return;
            }
            Assert.Fail("Expected exception not thrown");
            
        }

        [TestMethod]
        public void Initialize_With_Not_Exe()
        {
            WorkerProcess process;
            try
            {
                // Setup and Action
                process = new WorkerProcess(NON_EXE_EXE);
            }
            catch (WorkProcessException)
            {
                return;
            }
            Assert.Fail("Expected exception not thrown");

        }

    }
}
