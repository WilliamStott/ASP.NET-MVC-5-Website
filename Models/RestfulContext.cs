using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Assignment.Models
{
    public class RestfulContext : DbContext
    {
        //Sets the database context for the Restful controller
        public RestfulContext() : base("name=dbConnection")
        {
        }

        public System.Data.Entity.DbSet<CourseInfo> CourseInfoes { get; set; }
    }
}
