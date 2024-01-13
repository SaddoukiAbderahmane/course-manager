using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace coursemanagement.Models
{
    public class ContextDb:DbContext
    {
        public ContextDb():base("name = defaultconection") { }
        public DbSet<Course>courses  { get; set; }
        public DbSet<Admin>Admines{ get; set; }

    }
}