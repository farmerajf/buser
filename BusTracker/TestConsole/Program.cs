using System;
using System.Collections.Generic;

namespace BusTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var route = new List<string>
                {
                    "0170SGA10050",
                    "0170SGA10049",
                    "0170SGB20001",
                    "0170SGB20002",
                    "0170SGB20003"
                };

                var tracker = new Buser();

                for (var stop = 0; stop < route.Count; stop++)
                {
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("Stop: " + route[stop]);

                    //var arrivals = tracker.GetBuses(route[stop]);
                    //foreach (var arrival in arrivals)
                    //{
                    //    Console.WriteLine("{0} | {1}mins", arrival.Code, arrival.MinutesToArrival);
                    //}
                    //Console.WriteLine();
                }

                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
