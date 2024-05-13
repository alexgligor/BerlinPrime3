namespace Berlin.Domain.Entities
{
    public class Bill : BaseDbObject // Chitanta
    {
        //Use Title for Serie and Description for number

        public string? QRLink { get; set; }
        public int ReceiptId { get; set; }

        public Receipt Receipt { get; set; }

    }
}
