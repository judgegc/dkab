using System;
using System.Diagnostics;
using System.IO;

namespace dkab.Helpers
{
    class Logger
    {
        public static void Write(string message)
        {
            File.AppendAllText(@"dkab.log", string.Format("[{0}] - {1}{2}", DateTime.Now.ToString("h:mm:ss"), message, Environment.NewLine));
        }

        public static void StackTrace(Exception ex)
        {
            Write(ex.Message);
            var frame = new StackTrace(ex, true).GetFrame(0);
            Write(string.Format("FileName: {0} LineNumber: {1} ColumnNumber: {2} Method: {3} Class: {4} ", frame.GetFileName(), frame.GetFileLineNumber(), frame.GetFileColumnNumber(), frame.GetMethod(), frame.GetMethod().DeclaringType));            
        }
    }
}
