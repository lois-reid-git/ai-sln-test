using System;
using System.Diagnostics;
using System.IO;

namespace AiCustomTaskScheduler.Work
{
    /// <summary>
    /// A wrapper class for triggering the external executable worker process as a new process. 
    /// </summary>
    public class WorkerProcess
    {
        public readonly string executablePath = "";

        public WorkerProcess(string executablePath)
        {
            this.executablePath = this.ValidatePath(executablePath);
        }

        /// <summary>
        /// Validates that the exe path is a .exe and exists.
        /// </summary>
        /// <param name="pathToExe"></param>
        /// <returns></returns>
        private string ValidatePath(string pathToExe)
        {
            // Check file exists, and that it's an .exe
            if (File.Exists(pathToExe))
            {
                if (Path.GetExtension(pathToExe) == ".exe")
                {
                    return pathToExe;
                }
                else
                {
                    string message = String.Format("Configured executable has unexpected extension. Expecting 'exe'. Found: {0}", Path.GetExtension(pathToExe));
                    throw new WorkProcessException(message);
                }
            }
            else
            {
                throw new FileNotFoundException(String.Format("Executable not found: {0}", pathToExe));
            }
        }

        /// <summary>
        /// Triggers the external process executable. Process will be started with window minimized.
        /// </summary>
        public void TriggerProcess()
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Minimized,
                    FileName = this.executablePath
                }
            };
            process.Start();
        }

    }
}
