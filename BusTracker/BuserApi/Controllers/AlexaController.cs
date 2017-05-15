using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BusTracker;

namespace BuserApi.Controllers
{
    public class AlexaController : ApiController
    {
        [HttpGet]
        [HttpPost]
        public dynamic Get(dynamic request)
        {
            var buser = new Buser();
            var buses = buser.GetBuses("0170SGB20003");
            var next48Bus = buses.FirstOrDefault(x => x.Code == "48" && x.HasMinutesToArrival);
            var nextX48Bus = buses.FirstOrDefault(x => x.Code == "X48" && x.HasMinutesToArrival);

            var firstBus = GetFirstBus(next48Bus, nextX48Bus);
            var secondBus = firstBus == next48Bus ? nextX48Bus : next48Bus;

            string response;
            if (firstBus != null)
            {
                response = $"Your next bus is the number {firstBus.Code} in {firstBus.MinutesToArrival} minutes. ";
                if (secondBus != null)
                {
                    response += $"There is also a {secondBus.Code} in {secondBus.MinutesToArrival} minutes.";
                }
                else
                {
                    if (firstBus.Code == "48")
                    {
                        response += "There are no X48 buses coming soon.";
                    }
                }
            }
            else
            {
                response = "I'm not sure at the moment";
            }

            return new
            {
                version="1.0",
                sessionAttributes = new {},
                response = new
                {
                    outputSpeech = new {type="PlainText", text= response }
                },
                card = new
                {
                    type=   "Simple",
                    title="Test",
                    content = "Hello World"
                },
                shouldEndSession = "true"
            };
        }

        private Bus GetFirstBus(Bus bus1, Bus bus2)
        {
            if (bus1 != null && bus2 == null) return bus1;
            if (bus1 == null && bus2 != null) return bus2;

            if (bus1.MinutesToArrival < bus2.MinutesToArrival) return bus1;

            return bus2;
        }
    }
}