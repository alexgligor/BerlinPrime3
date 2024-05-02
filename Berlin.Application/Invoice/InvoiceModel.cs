namespace Berlin.Application.Invoice
{
    public class InvoiceModel
    {
        public int InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }

        public CompanyDetails SellerAddress { get; set; }
        public CompanyDetails CustomerAddress { get; set; }

        public List<OrderItem> Items { get; set; }
        public string Comments { get; set; }
    }

    public class OrderItem
    {
        public string Name { get; set; }
        public string UM { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class CompanyDetails
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string CIF { get; set; }
        public string Bank { get; set; }
        public string IBAN { get; set; }
        public object Email { get; set; }
        public string Phone { get; set; }
        public string SocialCapital { get; set; }
        public string Delegate { get; set; }
        public string Comments { get; set; }

    }
}
