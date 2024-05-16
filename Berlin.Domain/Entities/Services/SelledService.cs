namespace Berlin.Domain.Entities
{
    public class SelledService: BaseDbObject   
    {  
        public float Price { get; set; }

        public int Count { get; set; } = 1;
        public int UserId { get; set; }

        public User User { get; set; }

        public int ReceiptId { get; set; }

        public Receipt Receipt { get; set; }

        public Service Service { get; set; }    

        public int ServiceId { get; set; }

        public void Load(User user, Receipt receipt)
        {
            Id = 0;
            User = user;
            Receipt = receipt;
            UserId = user.Id;
            ReceiptId = receipt.Id;
        }
        public void Load(User user, Service service)
        {
            Id = 0;
            User = user;
            this.Service = service;
            Price = service.Price;

            ServiceId = service.Id;
            UserId = user.Id;
        }

    }


}
