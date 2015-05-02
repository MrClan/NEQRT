using EQR_Go2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EQR_Go2.Views.Shared
{
    public static class FixedObjects
    {
        public static IEnumerable<Site> SiteList
        {
            get
            {
                return new Site().All().Select(s => new Site { Name = s.Name, ID = s.ID }).OrderBy(si=>si.Name);
            }
        }

        public static IEnumerable<Commodity> CommodityList
        {
            get
            {
                return new Commodity().All().Select(s => new Commodity { ID = s.ID, Name = s.Name }).OrderBy(c=>c.Name);
            }
        }

        public static Dictionary<RoleList, string> Role = new Dictionary<RoleList, string> {
            {RoleList.Volunteer, "Volunteer"},
            {RoleList.Victim,"Victim"},
            {RoleList.MedicalStaff,"Medical Staff"},
            {RoleList.Police,"Police"}
        };

        public enum RoleList
        {
            Volunteer,
            Victim,
            MedicalStaff,
            Police
        }
    }
}