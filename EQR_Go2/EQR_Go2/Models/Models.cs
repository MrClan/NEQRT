using Massive.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EQR_Go2.Models
{

    public class Team : DynamicModel
    {
        public Team() : base("SQLiteCon", "Team", "ID") { }
        public long ID { get; set; }
        public string TeamName { get; set; }
        public long MemCount { get; set; }
        public string PrimaryContact { get; set; }
        public string Destination { get; set; }
        public string DepartureFrom { get; set; }
        public DateTime DepartureOn { get; set; }
        public DateTime ETA { get; set; }
    }

    public class TeamCommodityDetails : DynamicModel
    {
        public TeamCommodityDetails() : base("SQLiteCon", "TeamCommodityDetails", "ID") { }
        public long ID { get; set; }
        public long CommodityID { get; set; }
        public long TeamID { get; set; }
        public long Count { get; set; }
    }

    public class Person : DynamicModel
    {
        public Person() : base("SQLiteCon", "Person", "ID") { }
        public long ID { get; set; }
        public long TeamID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string OtherDetails { get; set; }
        public string Role { get; set; }
    }

}