namespace Berlin.Domain.Entities
{
    public class Site: BaseDbObject // punct de lucru
    {
        public ICollection<SiteDivision> Divisions { get; set; } = new List<SiteDivision>();
        public ICollection<Device> Devices { get; set; } = new List<Device>();
        public ICollection<ProductHistory> ProductHistories { get; set; } = new List<ProductHistory>();
        public ICollection<SiteUser> Users { get; set; } = new List<SiteUser>();
        public ICollection<ProductSite> Products { get; set; } = new List<ProductSite>();
        public BillDetails BillDetails { get; set; }
        public int? BillDetailsId { get; set; }
        public Company Company { get; set; }
        public int? CompanyId { get; set; }

    }
}
