using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTracker
{
    public class Bus
    {
        public string Code { get; set; }
        public bool HasMinutesToArrival { get; set; }
        public int MinutesToArrival { get; set; }
        public DateTime ArrivateTime { get; set; }
    }
}
