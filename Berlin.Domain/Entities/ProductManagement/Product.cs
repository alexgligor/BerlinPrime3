using Berlin.Domain.Entities.ProductManagement;

namespace Berlin.Domain.Entities
{
    public class Product: BaseDbObject
    {
        public int Count { get; set; }
        public int? ServiceId { get; set; }
        public Service? Service { get; set; }
        public float Price { get; set; }
        public int? SiteId { get; set; }
        public Site? Site { get; set; }
        public ICollection<ProductHistory> ProductHistores { get; set; } = new List<ProductHistory>();
        public ICollection<ProductService> Services { get; set; } = new List<ProductService>();
        public ICollection<ProductSite> SiteProducts { get; set; } = new List<ProductSite>();


    }
}
