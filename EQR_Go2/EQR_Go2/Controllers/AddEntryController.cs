using EQR_Go2.Models;
using EQR_Go2.Views.Shared;
using Massive.SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EQR_Go2.Controllers
{
    public class AddEntryController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.CurMenu = "addentry";
            ViewBag.OpSuccessMsg = "";
            ViewBag.HeaderMsg = "Going on a help mission? Help channelize the relief efforts by letting others know about your trip.";
            return View();
        }

        public ActionResult Suggest()
        {
            ViewBag.CurMenu = "suggest";
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            ViewBag.CurMenu = "addentry";
            ViewBag.HeaderMsg = "Going for help ? Fill up the form below to help others know what needs to be done";
            var teamTable = new Team();
            var newTeam = teamTable.CreateFrom(fc);
            var teamId = teamTable.Insert(newTeam);
            // parse member list from fc
            var personTable = new Person();
            foreach (var mKey in fc.AllKeys.Where(k => k.StartsWith("m_")))
            {
                string index = mKey.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1];
                var p = new { Name = fc[mKey], Contact = fc["mn_" + index], Role = FixedObjects.Role[FixedObjects.RoleList.Volunteer], TeamID = teamId };
                personTable.Insert(p);
            }


            var teamCommodityDetailsTable = new TeamCommodityDetails();
            // parse original commodity details from fc
            foreach (var cKey in fc.AllKeys.Where(k => k.StartsWith("oc_")))
            {
                string index = cKey.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1];
                long curCount = 0;
                long.TryParse(fc["occount_" + index],out curCount);
                var p = new { CommodityID = long.Parse(fc[cKey]), Count = curCount, TeamID = teamId };
                teamCommodityDetailsTable.Insert(p);
            }

            // parse newly added commodity details from fc
            foreach (var cKey in fc.AllKeys.Where(k => k.StartsWith("c_")))
            {
                string index = cKey.Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[1];
                var p = new { CommodityID = long.Parse(fc[cKey]), Count = long.Parse(fc["ccount_" + index]), TeamID = teamId };
                teamCommodityDetailsTable.Insert(p);
            }

            ViewBag.OpSuccessMsg = "Details successfully submitted.";
            return View();
        }
        public ActionResult New()
        {

            return View();
        }
    }
}