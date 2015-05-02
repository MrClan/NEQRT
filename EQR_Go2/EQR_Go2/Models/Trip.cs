using Massive.SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EQR_Go2.Models
{
    public class Trip : DynamicModel
    {
        public Trip() : base("SqliteCon", "trip", "ID") { }
        public int ID { get; set; }
        public int NoOfMembers { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public int Destination { get; set; }
        public int StartPlace { get; set; }
    }

    public class Commodity : DynamicModel
    {
        public Commodity() : base("SqliteCon", "commodities", "ID") { }
        public long ID { get; set; }
        [Required,MaxLength(39, ErrorMessage="Too Long")]
        public string Name { get; set; }
        //[Required(ErrorMessage = "*")]
        //public long HaveWithUs { get; set; }

        //[Required(ErrorMessage="*")]
        //public long NeedMore { get; set; }

    }

    public class Site : DynamicModel
    {
        public Site() : base("SqliteCon", "sites", "ID") { }
        public long ID { get; set; }
        public string Name { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }
    }

    public class EFRepo
    {

    }

    public class EFContext<T> : DbContext where T : class
    {
        public EFContext()
        {
            //Turn off the migrations, since this is not a code first scenario
            Database.SetInitializer<EFContext<T>>(null);
        }

        public DbSet<T> EntitySet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}