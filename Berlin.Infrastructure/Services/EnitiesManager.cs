using Berlin.Domain.Entities;
using Berlin.Domain.Entities.Interfaces;

namespace Berlin.Infrastructure
{
    public class EnitiesManager : IEntitiesManager
    {
        IGenericService<Site> siteService;
        IGenericService<Division> divisionService;

        public EnitiesManager(
            IGenericService<Site> siteService, 
            IGenericService<Division> divisionService
            )
        {
            this.siteService = siteService;
            this.divisionService = divisionService;

        }

        public async Task AddRemoveDevisionFromSite(Site site,Division division)
        {
            SiteDivision? sitef = null;
            try
            {
                sitef = site.Divisions.First(s => s.DivisionId == division.Id);
            }
            catch { }

            if (sitef != null)
                site.Divisions.Remove(sitef);
            else
                site.Divisions.Add(new SiteDivision() { SiteId = site.Id, Division = division, DivisionId = division.Id });

            await siteService.Update(site);
        }

        public async Task AddSite(Site site)
        {
            var post = site.Title.Substring(0, 3).ToUpper() + DateTime.Now.Year;
            if(site.BillDetails == null)
                site.BillDetails = new BillDetails()
                {
                    Title = site.Title,
                    DevizNr = 1,
                    InvoiceNr = 1,
                    BillNr = 1,
                    DevizSerie = "DE" + post,
                    BillSerie = "CH" + post,
                    InvoiceSerie = "FA" + post,
                    SiteId = site.Id

                };

            if (site.Company == null)
                site.Company = new Company()
                {
                    Title = "Compania ta aici",
                    CIF = "12345678",
                    RegCom = ".......",
                    Address = ".......",
                    Phone = ".......",
                    SocialCapital = "200RON",
                    Email = ".......",
                    Bank = ".......",
                    IBAN = ".......",
                };
                await siteService.Add(site);
        }

        public async Task DeleteSite(Site site)
        {
            await siteService.Remove(site.Id);
        }

        public async Task<List<Division>> GetDivisions()
        {
            return await divisionService.GetAll(d => d.Sites);
        }

        public async  Task<List<Site>> GetSites()
        {
            return await siteService.GetAll(s => s.Divisions, s => s.Company, s => s.BillDetails);
        }

        public async Task UpdateSite(Site site)
        {
            await siteService.Update(site);
        }
    }
}
