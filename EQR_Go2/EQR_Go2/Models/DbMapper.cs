using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace EQR_Go2.Models
{
    public class DbMapper
    {
        string dbPath = "";//HttpContext.Server.MapPath("~/Dependencies/EQR_Go2.sqlite");
        IDbConnection dbConnection;
        public DbMapper()
        {
            dbConnection = new SQLiteConnection(String.Format("Data Source={0};Version=3;", dbPath));
        }


    }
}