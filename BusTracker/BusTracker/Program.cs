using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace BusTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var tracker = new Buser();
            var serivceITems = tracker.GetBuses("0170SGB20003");

            foreach (var serviceItem in serivceITems)
            {
                Console.WriteLine(serviceItem.Code + " | " + serviceItem.MinutesToArrival);
            }

            Console.ReadLine();
        }
    }
}
