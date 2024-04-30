using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Domain.Entities
{
    public class Service:BaseDbObject
    {
        public ServiceType ServiceType { get; set; }
        public int ServiceTypeId { get; set; }
        public float Price { get; set; }

        public string? ImageUrl { get; set; }
    }
}
