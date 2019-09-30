using System;
using System.IO;
using System.Text;

namespace ACoreX.Logger.Base
{
    internal class FileLogger : ILogger
    {
        public void WriteMessage(string text)
        {
            WriteMessageToFile(text);
        }

        public void WriteMessageToFile(string text, string logPath = (@"c:\log\file.txt"), bool addToStart = false)
        {
            StringBuilder sb = new StringBuilder();
            string str = "";
            if (!File.Exists(logPath))
            {
                File.Create(logPath).Dispose();

            }
            else if (addToStart)
            {
                using (StreamReader sreader = new StreamReader(logPath))
                {
                    str = sreader.ReadToEnd();
                }
                File.Delete(logPath);

            }
            sb.AppendFormat("Time: {0} Message: {1}", DateTime.Now.ToString(), text);
            sb.AppendLine();
            sb.Append(str);
            using (StreamWriter logWriter = new StreamWriter(logPath, true))
            {

                logWriter.Write(sb.ToString());
            }

        }
    }
}
