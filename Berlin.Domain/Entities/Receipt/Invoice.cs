namespace Berlin.Domain.Entities
{
    public class Invoice: BaseDbObject
    {
        //Use Title for Serie and Description for number

        public string? Disclamer { get; set; }
        public string? QRLink { get; set; }

        public int ReceiptId { get; set; }

        public Receipt Receipt { get; set; }

    }

}
