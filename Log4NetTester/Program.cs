﻿using System.Collections.Generic;
using log4net;

namespace Log4NetTester
{
    using System;
    using System.Threading;

    class Program
    {
        private static readonly ILog log = LogManager.GetLogger("Log4NetTester");

        private static readonly Random random = new Random();

        private static readonly List<string> reasons = new List<string>
                                                           {
                                                               "Starting system",
                                                               "Closing system",
                                                               "Data exchange started",
                                                               "Unknown issue encountered"
                                                           };

        private static readonly List<string> sources = new List<string>
                                                           {
                                                               "Foo",
                                                               "Bar",
                                                               "LongSystemName",
                                                               "Kernel32"
                                                           };


        static void Main()
        {
            int i = 0;

            int smallestSleep = 10;
            int biggestSleep = 20;

            while(i<100000)
            {
                // Randomly generate a message:
                string text = RandomMessage(i++);

                // Randomly assign a message
                LogMessage(text);

                Thread.Sleep(random.Next(smallestSleep, biggestSleep));
            }
        }

        private static void LogMessage(string text)
        {
            int randomType = random.Next(0, 5);

            switch(randomType)
            {
                case 0: log.Error(text);
                    break;
                case 1: log.Fatal(text);
                    break;
                case 2: log.Info(text);
                    break;
                case 3: log.Warn(text);
                    break;
                default: log.Debug(text);
                    break;
            }
        }

        private static string RandomMessage(int i)
        {
#if !TESTING_MESSAGE_THROUGHPUT
            int randomNumber = random.Next(0, 5);
#else
            int randomNumber = Int32.MaxValue;
#endif
            switch (randomNumber)
            {
                case 0:
                    return string.Format("Message {0}", i);
                case 1:
                    return string.Format("Src:'{0}', Msg:'{1} - {2}'", RandomSrc(), RandomReason(), i);
                case 2:
                    return string.Format("[{0}] {1} - {2}", RandomSrc(), RandomReason(), i);
                case 3:
                    return string.Format("[SimulationTime] {0} ({1})", RandomReason(), i);
                default:
                    return i.ToString();
            }
        }

        private static string RandomReason()
        {
            return reasons[random.Next(reasons.Count)];
        }

        private static string RandomSrc()
        {
            return sources[random.Next(sources.Count)];
        }
    }
}

