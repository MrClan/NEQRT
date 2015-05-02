using EQR_Go2.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EQR_Go2.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        public JsonResult InsertCommodity(string name)
        {
            try
            {
                var comTable = new Commodity();
                TextInfo ti = new CultureInfo("en-US", false).TextInfo;
                name = ti.ToTitleCase(name);
                dynamic newCom = new { Name = name };
                var newId = comTable.Insert(newCom);
                return Json(new { msg = "", success = true, id = newId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(new { msg = exc.Message, success = false}, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult InsertDestination(string name)
        {
            try
            {
                var siteTable = new Site();
                TextInfo ti = new CultureInfo("en-US", false).TextInfo;
                name = ti.ToTitleCase(name);
                var s = new { Name = name, AddedBy = "User", AddedOn = DateTime.Now, UpdatedBy = "User", UpdatedOn = DateTime.Now };
                var newId = siteTable.Insert(s);
                return Json(new {msg = "", success = true, id = newId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exc)
            {
                return Json(new { msg = exc.Message, success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}