using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TwitchTools
{
    class Utilities
    {
#if DEBUG

        public void DebugLog(string log)
        {
            const string logPath = @".\Debug.log";
            string timeStamp = DateTime.Now.ToString();
            
            File.AppendAllText(logPath, (timeStamp + System.Environment.NewLine));
            File.AppendAllText(logPath, (log + System.Environment.NewLine));

            return;
        }
#else
        public void DebugLog(string log)
        {
            return;
        }

#endif

        public void WriteFile(string text, string fileName)
        {
            //May need to process things here so making a wrapper function
            //So just write the file, not WriteAllText overwrites any prior data
            File.WriteAllText(fileName, text);
        }
    }
}
