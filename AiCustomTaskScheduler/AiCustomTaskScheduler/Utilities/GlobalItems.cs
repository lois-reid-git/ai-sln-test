using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiCustomTaskScheduler.Utilities
{
    /// <summary>
    /// Contains useful strings / items for use globally within this program.
    /// </summary>
    public static class GlobalItems
    {
        /// <summary>
        /// ISO 8601 UTC (zero-offset) string format. 
        /// yyyy-MM-ddThh:mm:ssZ
        /// </summary>
        public static string Iso8601UtcFormat
        {
            get
            {
                return "yyyy-MM-ddThh:mm:ssZ";
            }
        }

    }
}
