using System;
using System.Collections.Generic;
using System.Text;

namespace BLL1.Models
{
    public interface IBaseData
    {
        Guid ID { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
        bool Deleted { get; set; }
    }
    public class BaseData
    {
        public Guid ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Deleted { get; set; }
    }
}
