using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BLL1.Models
{
    public class Products : BaseData
    {
       
        public string Name { get; set; }
        public string Address { get; set; }
        public Categorys Category { get; set; }
        [ForeignKey("Category")]
        public Guid IDCategory { get; set; }
    }
    public class ProductDTO : BaseData
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string CategoryName { get; set; }
    }
}