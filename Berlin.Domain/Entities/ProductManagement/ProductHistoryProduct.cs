using System;

namespace Berlin.Domain.Entities
{
    public class ProductHistoryProduct : BaseDbObject
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public ProductHistory ProductHistory { get; set; }
        public int ProductHistoryId { get; set; }

    }
}
