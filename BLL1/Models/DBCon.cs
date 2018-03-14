using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace BLL1.Models
{

    public class DBCon<T> : IdentityDbContext<ApplicationUser> where T : class
    {
        public DBCon() : base("DefaultConnection")
        { }
        public DbSet<T> Table { get; set; }
    }
}
