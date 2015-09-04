using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusTracker;

namespace BuserApi.Controllers
{
    public class BuserController : ApiController
    {
        // GET api/values/5
        public IEnumerable<Bus> Get(string id)
        {
            var buser = new Buser();
            var buses = buser.GetBuses(id);

            return buses;
        }
    }
}
