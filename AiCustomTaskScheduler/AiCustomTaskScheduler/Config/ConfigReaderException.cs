using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AiCustomTaskScheduler.Config
{
    public class ConfigReaderException : Exception
    {
        public ConfigReaderException()
        {
        }

        public ConfigReaderException(string message) : base(message)
        {
        }

        public ConfigReaderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfigReaderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
