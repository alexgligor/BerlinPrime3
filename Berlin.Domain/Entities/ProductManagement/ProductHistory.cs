using System;

namespace Berlin.Domain.Entities
{
    public class ProductHistory: BaseDbObject
    {
        public Product Product { get; set; }
        public int? ProductId { get; set; }
        public int Count { get; set; }
        public float Price { get; set; }

        public int? SiteId { get; set; } 
        public Site Site { get; set; }


    }
}
