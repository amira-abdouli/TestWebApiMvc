using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestWebApiMvc.Models
{
    public class Products
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}