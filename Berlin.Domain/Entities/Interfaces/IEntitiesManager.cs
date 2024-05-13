namespace Berlin.Domain.Entities.Interfaces
{
    public  interface IEntitiesManager
    {
        Task AddSite(Site site);
        Task UpdateSite(Site site);
        Task DeleteSite(Site site);
        Task AddRemoveDevisionFromSite(Site site, Division division);
        Task<List<Site>> GetSites();
        Task<List<Division>> GetDivisions();
    }
}
