using Berlin.Domain.Entities.ProductManagement;
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

        public string UM { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }
        public ICollection<ProductService> Products { get; set; } = new List<ProductService>();

    }
}
