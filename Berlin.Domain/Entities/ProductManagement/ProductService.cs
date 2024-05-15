namespace Berlin.Domain.Entities.ProductManagement
{
    public class ProductService
    {
        public int Multiplier { get; set; } = 1;
        public Service Service { get; set; }
        public int ServiceId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }  

    }
}
