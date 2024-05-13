using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Domain.Entities
{
    public class ServiceProduct
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }

        public Service Service { get; set; }
        public int ServiceId { get; set; }
    }
}
