using Berlin.Domain.Entities;
using Berlin.Domain.Entities.Interfaces;

namespace Berlin.Infrastructure
{
    public class EnitiesManager : IEntitiesManager
    {
        IGenericService<Site> siteService;
        IGenericService<Division> divisionService;
        IGenericService<Receipt> receiptService;
        IGenericService<Service> serviceService;
        IGenericService<Product> productService;
        IGenericService<ProductHistory> productHistoryService;

        public EnitiesManager(
            IGenericService<Site> siteService, 
            IGenericService<Division> divisionService,
            IGenericService<Receipt> receiptService,
            IGenericService<Service> serviceService,
            IGenericService<Product> productService,
            IGenericService<ProductHistory> productHistoryService
            )
        {
            this.siteService = siteService;
            this.divisionService = divisionService;
            this.receiptService = receiptService;
            this.serviceService = serviceService;
            this.productService = productService;
            this.productHistoryService = productHistoryService;
        }

        public async Task ReceiptAdd(Receipt receipt, List<SelledService> selledServices, string details)
        {
            foreach (var serv in selledServices)
            {
                serv.Title = details;
                if (serv.ReceiptId == 0)
                {
                    serv.CreateDate = DateTime.Now;
                    serv.UpdateDate = DateTime.Now;
                    serv.Receipt = receipt;
                    
                }
                else
                    serv.UpdateDate = DateTime.Now;

                var service = await serviceService.Get(serv.Service.Id, s => s.Products);
                if (service != null)
                {
                    foreach (var product in service.Products)
                    {
                        await ProductCountDown(product.ProductId,receipt.SiteId, serv.Service.Id, serv.Count);
                    }
                }
                await serviceService.Update(service);

            }


            receipt.SelledServices = selledServices;

            if (receipt.Id == 0)
                await receiptService.Add(receipt);
            else
                await receiptService.Update(receipt);

        }

        public async Task SiteAddRemoveDevision(Site site,Division division)
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

        public async Task SiteAdd(Site site)
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

        public async Task SiteDelete(Site site)
        {
            await siteService.Remove(site.Id);
        }

        public async Task<List<Division>> DivisionsGet()
        {
            return await divisionService.GetAll(d => d.Sites);
        }

        public async  Task<List<Site>> SiteGetAll()
        {
            return await siteService.GetAll(s => s.Divisions, s => s.Company, s => s.BillDetails);
        }

        public async Task<List<Site>> SiteGetAllDefaultState()
        {
            return await siteService.GetAll();
        }

        public async Task SiteUpdate(Site site)
        {
            await siteService.Update(site);
        }

        public async Task ProductCountDown(int productId, int siteId, int serviceId, int number)
        {
            var productUpdated = await productService.Get(productId,s=>s.SiteProducts, s => s.Services);
            if(productUpdated == null) 
                return;

            var productsite = productUpdated.SiteProducts.Where(s => s.SiteId == siteId).FirstOrDefault();
            if(productsite == null)
                return ;

            var serviceProd = productUpdated.Services.First(s => s.ServiceId == serviceId);
            productUpdated.Count = 0;
            foreach (var prodSite in productUpdated.SiteProducts)
            {
                if (prodSite.SiteId == siteId)
                    prodSite.Count-=(number* serviceProd.Multiplier);

                productUpdated.Count += prodSite.Count;
            }
            productUpdated.UpdateDate = DateTime.Now;

            await ProductHistory(productUpdated);

            await productService.Update(productUpdated);
        }

        public async Task<Product> ProductAdd(Product product)
        {
            if (product.SiteProducts != null)
            {
                foreach (var siteProduct in product.SiteProducts)
                    product.Count += siteProduct.Count;

                await ProductHistory(product);
            }
            return await productService.Add(product);
        }

        public async Task ProductRemove(Product product)
        {
            await productService.Remove(product);
        }

        public async Task<List<Product>> ProductGetAll()
        {
            return await productService.GetAll(p => p.SiteProducts);
        }

        private async Task ProductHistory(Product product)
        {
            await productHistoryService.Add(
                   new ProductHistory()
                   {
                       Count = product.Count,
                       Title = product.Title,
                       SiteId = product.SiteId,
                       ProductId = product.Id,
                       CreateDate = DateTime.Now,
                       Price = product.Price
                   });
        }
        public async Task ProductUpdate(Product product)
        {
            product.Count = 0;
            if (product.SiteProducts != null)
            {
                foreach (var siteProduct in product.SiteProducts)
                    product.Count += siteProduct.Count;

                await ProductHistory(product);
            }

            await productService.Update(product);
        }

        public async  Task<Product> ProductGet(int id)
        {
            return  await productService.Get(id);
        }

        public Task<List<Service>> ServiceGetAll()
        {
            return serviceService.GetAll(s=>s.ServiceType);
        }
    }
}
