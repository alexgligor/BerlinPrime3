namespace Berlin.Domain.Entities.Interfaces
{
    public  interface IEntitiesManager
    {
        Task<List<Division>> DivisionsGet();
        Task ReceiptAdd(Receipt receipt, List<SelledService> selledServices, string details);
        Task ProductCountDown(int productId,int siteId,int serviceId, int number);
        Task<Product> ProductAdd(Product product);
        Task<Product> ProductGet(int id);
        Task ProductRemove(Product product);
        Task ProductUpdate(Product product);
        Task<List<Product>> ProductGetAll();
        Task SiteAdd(Site site);
        Task SiteUpdate(Site site);
        Task SiteDelete(Site site);
        Task SiteAddRemoveDevision(Site site, Division division);
        Task<List<Site>> SiteGetAll();

        Task<List<Site>> SiteGetAllDefaultState();
        Task<List<Service>> ServiceGetAll();

    }
}
