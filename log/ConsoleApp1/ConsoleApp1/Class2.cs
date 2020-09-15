using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    public static class Class2
    {
        public static void Run()
        {
            var source = new TraceSource("trace", SourceLevels.All);

            source.Listeners.Add(new ConsoleTraceListener());

            using var fileStream = File.OpenWrite("log.txt");
            source.Listeners.Add(new TextWriterTraceListener(fileStream));

            var eventTypes = (TraceEventType[])Enum.GetValues(typeof(TraceEventType));
            var eventId = 1;
            foreach (var item in eventTypes)
            {
                source.TraceEvent(item, eventId++, $"{item}信息");
                source.Flush();
            }
        }
    }
}
