﻿using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System;

namespace BLL1.Models
{
    public class Categorys:BaseData
    {
        //public Guid ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Products> Listproducts { get; set; }
    }
}