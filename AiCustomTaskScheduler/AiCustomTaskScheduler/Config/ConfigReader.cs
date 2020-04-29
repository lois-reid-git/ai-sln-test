using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiCustomTaskScheduler.Config
{
    /// <summary>
    /// Class providing methods to read and check the config file
    /// </summary>
    public class ConfigReader
    {
        public JConfig taskConfigurations;
        public readonly string configPath;
        public const string READ_ERROR_MSG = "Unable to read JSON config file.";

        public ConfigReader(string configFilePath)
        {
            this.ValidateFileType(configFilePath);
            this.configPath = configFilePath;
        }

        /// <summary>
        /// Reads JSON config file into JSON class, returning the JSON class to the caller.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ConfigReaderException">Thrown when there is an exception reading or converting file.</exception>
        public void ReadConfig()
        {
            try
            {
                // Check that the path exists and has .json extension
                using (StreamReader stReader = File.OpenText(this.configPath))
                {
                    string jStrConfig = stReader.ReadToEnd();
                    this.taskConfigurations = JsonConvert.DeserializeObject<JConfig>(jStrConfig);
                }
            }
            catch(OutOfMemoryException e)
            {
                throw new ConfigReaderException(READ_ERROR_MSG, e);
            }
            catch(IOException e)
            {
                throw new ConfigReaderException(READ_ERROR_MSG, e);
            }
            catch(JsonReaderException e)
            {
                throw new ConfigReaderException(READ_ERROR_MSG, e);
            }
           
        }

        /// <summary>
        /// Validates that the file path exists and is of JSON extension type.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        private void ValidateFileType(string fullPath)
        {
            if (File.Exists(fullPath))
            {
                if (Path.GetExtension(fullPath) != ".json")
                {
                    string message = String.Format("JSON config file has unexpected extension. Expecting '.json'. Found: {0}", Path.GetExtension(fullPath));
                    throw new ConfigReaderException(message);
                }
            }
            else
            {
                throw new FileNotFoundException(String.Format("JSON config file not found: {0}", fullPath));
            }
        }
    }
}
