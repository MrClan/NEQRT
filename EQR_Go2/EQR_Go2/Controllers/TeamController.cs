using EQR_Go2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EQR_Go2.Controllers
{
    public class TeamController : Controller
    {
        public ActionResult Details(string id)
        {
            ViewBag.CurMenu = "Team Details";
            string query = "SELECT SELECT * FROM team {0}";
            if (!String.IsNullOrWhiteSpace(id) && id.ToLower() != "all")
            {
                id = id.Trim();
                query = String.Format(query, "Where destination='" + id + "'");// FixedObjects.SiteList.First().Name;
            }
            else
                query = String.Format(query, "");
            ViewBag.HeaderMsg = "Here's the list of teams who have been on relief missions";
            var tbl = new Team();
            var results = tbl.All();// tbl.Query(query, new object[] { }); // USE TABLE.QUERY TO FILTER TEAMS BY LOCATION
            var formattedResults = FormatResults(results);
            //if (!String.IsNullOrEmpty(id) && formattedResults.ContainsKey(id))
            //    formattedResults = new Dictionary<string, Dictionary<string, int>>() { { id, formattedResults[id] } };
            ViewBag.Results = formattedResults;
            //ViewBag.TeamDetails = teamDetails;
            ViewBag.SelectedValue = id;
            return View(formattedResults);
        }

        private List<Team> FormatResults(IEnumerable<dynamic> results)
        {
            List<Team> retVal = new List<Team>();
            foreach(var r in results)
            {
                retVal.Add(new Team {TeamName=r.TeamName, DepartureFrom = r.DepartureFrom, DepartureOn = r.DepartureOn, Destination = r.Destination, ETA = r.ETA, ID = r.ID, MemCount = r.MemCount, PrimaryContact = r.PrimaryContact });
            }
            return retVal;
        }
    }
}