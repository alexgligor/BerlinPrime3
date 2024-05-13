using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Domain.Entities
{
    public class SiteProduct
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }

        public Site Site { get; set; }
        public int SiteId { get; set; } 
    }
}
