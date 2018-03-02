using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApiMvc.Models
{
    public class models
    {
        ApplicationDbContext db = new ApplicationDbContext();
        IQueryable<Products> getall()
        {
            return db.Products;
        }
    }
}