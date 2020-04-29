using System;
using System.Runtime.Serialization;

namespace AiCustomTaskScheduler.Work
{
    [Serializable]
    public class WorkProcessException : Exception
    {
        public WorkProcessException()
        {
        }

        public WorkProcessException(string message) : base(message)
        {
        }

        public WorkProcessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WorkProcessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}