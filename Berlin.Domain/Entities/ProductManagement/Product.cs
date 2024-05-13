using System;

namespace Berlin.Domain.Entities
{
    public class Product: BaseDbObject
    {
        public int Count { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public float Price { get; set; }
        public int SiteId { get; set; }
        public Site Site { get; set; }
        public ICollection<ProductHistoryProduct> ProductHistores { get; set; } = new List<ProductHistoryProduct>();


    }
}
