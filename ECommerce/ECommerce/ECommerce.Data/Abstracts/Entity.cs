using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.Abstracts
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
