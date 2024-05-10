using Berlin.Domain.Entities;

namespace Berlin.Application.Invoice
{
    public class InvoiceModel
    {
        public Company SellerAddress;

        public List<SelledService> Items { get; set; }
        public Receipt Receipt { get; set; }
    }
}
