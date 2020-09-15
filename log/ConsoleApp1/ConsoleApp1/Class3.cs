using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    public static class Class3
    {
        public  static void Run()
        {
            const string fileName = "log1.csv";
            File.AppendAllText(fileName, $"SourceName,EventType,EventId,Message,N/A,ProcessId,N/A,ThreadId,DateTime,{Environment.NewLine}");

            using var fileStream = new FileStream(fileName, FileMode.Append);
            var listener = new DelimitedListTraceListener(fileStream)
            {
                Delimiter = ",",
                TraceOutputOptions = TraceOptions.DateTime | TraceOptions.ProcessId | TraceOptions.ThreadId
            };

            var source = new TraceSource("Trace", SourceLevels.All);
            source.Listeners.Add(new ConsoleTraceListener());
            source.Listeners.Add(listener);

            source.TraceEvent(TraceEventType.Warning, 1, $"{TraceEventType.Warning}信息");
            source.Flush();
        }
    }
}
