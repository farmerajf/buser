using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Script.Serialization;
using BusTracker;

namespace BuserApi.Controllers
{
    public class BuserController : ApiController
    {
        // GET api/values/5
        public string Get(string id, string apiKey)
        {
            if (apiKey != ConfigurationManager.AppSettings["ApiKey"])
            {
                var error = new {Error = "Invalid API key"};
                var errorJson = new JavaScriptSerializer().Serialize(error);
                return errorJson;
            }

            var buser = new Buser();
            var buses = buser.GetBuses(id);

            var json = new JavaScriptSerializer().Serialize(buses);
            return json;
        }
    }
}
