namespace Berlin.Domain.Entities
{
    public class ProductSite
    {
        public Site Site { get; set; }
        public int SiteId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }  

        public int Count { get; set; }

    }
}
