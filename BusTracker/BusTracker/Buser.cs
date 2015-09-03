using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace BusTracker
{
    public class Buser
    {
        //0170SGB20003
        private const string baseUrl = "http://travelwest.acisconnect.com/Popup_Content/WebDisplay/WebDisplay.aspx?stopRef=";

        public IEnumerable<Bus> GetBuses(string stopReference)
        {
            var html = GetHtml(stopReference);
            var serviceRows = GetServiceRows(html);
            var serviceItems = serviceRows.Select(MapToModel);

            return serviceItems;
        }

        private string GetHtml(string stopReference)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadString(baseUrl + stopReference);
            }
        }

        private IEnumerable<HtmlNode> GetServiceRows(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var tableRows = htmlDocument.DocumentNode.SelectNodes("//tr");
            var serviceRows =
                tableRows.Where(x => x.Attributes.Count > 0).Where(x => x.Attributes["class"].Value == "gridRow");

            return serviceRows;
        }

        private Bus MapToModel(HtmlNode serviceRow)
        {
            var serviceNumber =
                serviceRow.ChildNodes.Where(x => x.Attributes.Count > 0)
                    .First(x => x.Attributes["class"].Value == "gridServiceItem")
                    .InnerHtml;
            var serviceTime =
                serviceRow.ChildNodes.Where(x => x.Attributes.Count > 0)
                    .First(x => x.Attributes["class"].Value == "gridTimeItem")
                    .InnerHtml;

            var model = new Bus
            {
                Code = serviceNumber,
                HasMinutesToArrival = IsEstimate(serviceTime)
            };
            if (IsEstimate(serviceTime))
            {
                model.MinutesToArrival = int.Parse(serviceTime.Substring(0, serviceTime.Length - 5));
                model.ArrivateTime = DateTime.Now.AddMinutes(model.MinutesToArrival);
            }
            else
            {
                model.ArrivateTime = DateTime.Parse(serviceTime);
            }
             
            return model;
        }

        private bool IsEstimate(string serviceTime)
        {
            return serviceTime.EndsWith("Mins");
        }
    }
}
