using EQR_Go2.Models;
using EQR_Go2.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EQR_Go2.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Status(string id)
        {
            ViewBag.CurMenu = "dashboard";
            string query = "SELECT t.Destination, e.Name, tcd.Count FROM TeamCommodityDetails tcd INNER JOIN TEAM t ON t.ID = tcd.TeamID  INNER JOIN COmmodities e ON e.ID = tcd.CommodityID {0} ORDER BY e.Name";
            if (!String.IsNullOrWhiteSpace(id) && id.ToLower() != "all")
            {
                id = id.Trim();
                query = String.Format(query, "Where t.Destination = '" + id + "'");// FixedObjects.SiteList.First().Name;
            }
            else
                query = String.Format(query, "");
            ViewBag.HeaderMsg = "Here's the current status of the relief efforts based on recorded data";
            var tbl = new Person();
            var results = tbl.Query(query, new object[] { });
            var formattedResults = FormatResults(results);
            if (!String.IsNullOrEmpty(id) && formattedResults.ContainsKey(id))
                formattedResults = new Dictionary<string, Dictionary<string, int>>() { { id, formattedResults[id] } };
            ViewBag.Results = formattedResults;
            ViewBag.TeamDetails = teamDetails;
            ViewBag.SelectedValue = id;
            return View();
        }
        Dictionary<string, int> teamDetails = new Dictionary<string, int>();

        private Dictionary<string, Dictionary<string, int>> FormatResults(IEnumerable<dynamic> results)
        {
            Dictionary<string, Dictionary<string, int>> retVal = new Dictionary<string, Dictionary<string, int>>();
            var tbl = new Person();
            // turn the result into displayable format
            foreach (var r in results)
            {
                foreach (var location in FixedObjects.SiteList.Select(s => s.Name))
                {
                    if (!teamDetails.ContainsKey(location))
                    {
                        var visitCount = tbl.Scalar("SELECT COUNT(ID) from team where destination = '" + location + "'");
                        teamDetails.Add(location, int.Parse(visitCount.ToString()));
                    }
                    if (!retVal.ContainsKey(location))
                        retVal.Add(location, new Dictionary<string, int>());
                    if (r.Destination.ToString() == location)
                    {
                        foreach (var commodity in FixedObjects.CommodityList.Select(c => c.Name))
                        {
                            // populate data for this location
                            if (!retVal[location].ContainsKey(commodity))
                                retVal[location].Add(commodity, 0);
                            if (r.Name == commodity)
                                retVal[location][commodity] += int.Parse(r.Count.ToString());
                        }
                    }
                }
            }
            return retVal;
        }
    }
}