using Berlin.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Berlin.Infrastructure.Services
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options)
       : base(options)
        {

        }
        public DbSet<GenericArticle> GenericArticles { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Deviz> Devize { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<SelledService> SelledServices { get; set; }
        public DbSet<BillDetails> BillDetails { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductHistoryProduct>()
           .HasKey(ab => new { ab.ProductId, ab.ProductHistoryId });

            modelBuilder.Entity<ProductHistoryProduct>()
                   .HasOne(ab => ab.Product)
                   .WithMany(a => a.ProductHistores)
                   .HasForeignKey(ab => ab.ProductId);
            /// for trasability
            modelBuilder.Entity<SiteProduct>()
            .HasKey(ab => new { ab.SiteId, ab.ProductId });
            modelBuilder.Entity<SiteProduct>()
                    .HasOne(ab => ab.Site)
                    .WithMany(a => a.Products)
                    .HasForeignKey(ab => ab.ProductId);

            modelBuilder.Entity<ServiceProduct>()
            .HasKey(ab => new { ab.ServiceId, ab.ProductId });

            modelBuilder.Entity<ServiceProduct>()
                    .HasOne(ab => ab.Service)
                    .WithMany(a => a.Products)
                    .HasForeignKey(ab => ab.ProductId);

            modelBuilder.Entity<Company>()
                   .HasOne(ab => ab.Site)
                   .WithOne(a => a.Company)
                   .HasForeignKey<Site>(ab => ab.CompanyId);

            modelBuilder.Entity<CompanyDetails>()
                   .HasOne(ab => ab.Receipt)
                   .WithOne(a => a.ClientDetails)
                   .HasForeignKey<Receipt>(ab => ab.ClientDetailsId);

            modelBuilder.Entity<BillDetails>()
                   .HasOne(ab => ab.Site)
                   .WithOne(a => a.BillDetails)
                   .HasForeignKey<Site>(ab => ab.BillDetailsId);

            modelBuilder.Entity<Bill>()
                    .HasOne(ab => ab.Receipt)
                    .WithOne(a => a.Bill)
                    .HasForeignKey<Receipt>(ab => ab.BillId);

            modelBuilder.Entity<Deviz>()
                    .HasOne(ab => ab.Receipt)
                    .WithOne(a => a.Deviz)
                    .HasForeignKey<Receipt>(ab => ab.DevizId);

            modelBuilder.Entity<Invoice>()
                    .HasOne(ab => ab.Receipt)
                    .WithOne(a => a.Invoice)
                    .HasForeignKey<Receipt>(ab => ab.InvoiceId);
            {
                modelBuilder.Entity<SiteUser>()
             .HasKey(ab => new { ab.SiteId, ab.UserId });

                modelBuilder.Entity<SiteUser>()
                    .HasOne(ab => ab.Site)
                    .WithMany(a => a.Users)
                    .HasForeignKey(ab => ab.SiteId);

                modelBuilder.Entity<SiteUser>()
                    .HasOne(ab => ab.User)
                    .WithMany(b => b.Sites)
                    .HasForeignKey(ab => ab.UserId);
            }

            modelBuilder.Entity<Site>()
               .HasMany(a => a.Devices)
               .WithOne(b => b.Site)
               .HasForeignKey(b => b.SiteId);

            modelBuilder.Entity<Division>()
               .HasMany(a => a.ServiceTypes)
               .WithOne(b => b.Devision)
               .HasForeignKey(b => b.DevisionId);

            modelBuilder.Entity<ServiceType>()
               .HasMany(a => a.Services)
               .WithOne(b => b.ServiceType)
               .HasForeignKey(b => b.ServiceTypeId);

            {
                modelBuilder.Entity<SiteDivision>()
               .HasKey(ab => new { ab.DivisionId, ab.SiteId });

                modelBuilder.Entity<SiteDivision>()
                    .HasOne(ab => ab.Site)
                    .WithMany(a => a.Divisions)
                    .HasForeignKey(ab => ab.SiteId);

                modelBuilder.Entity<SiteDivision>()
                    .HasOne(ab => ab.Division)
                    .WithMany(b => b.Sites)
                    .HasForeignKey(ab => ab.DivisionId);
            }

            modelBuilder.Entity<GenericArticle>().HasData(
                new GenericArticle { Location = "Building A", Id = 1, Title = "Surub", Count = 100, Currency = "RON", Price = 2, Description = "16/8", CreateDate = DateTime.Now },
                new GenericArticle { Location = "Building B", Id = 2, Title = "Ruleta", Count = 80, Currency = "RON", Price = 17, Description = "5m,magnetica", CreateDate = DateTime.Now },
                new GenericArticle { Location = "Building A", Id = 3, Title = "Echer", Count = 60, Currency = "RON", Price = 6, Description = "40/40", CreateDate = DateTime.Now },
                new GenericArticle { Location = "Magazin", Id = 4, Title = "Dalta 20", Count = 120, Currency = "RON", Price = 2, Description = "Maner de cauciuc", CreateDate = DateTime.Now },
                new GenericArticle { Location = "Magazin", Id = 5, Title = "Drujba Hyundai", Count = 3, Currency = "RON", Price = 690, Description = "2CP", CreateDate = DateTime.Now }
            );

            modelBuilder.Entity<User>().HasData(
            new User() { Id = 1, Title = "Alex", Description = "Administrator" }
                );

            modelBuilder.Entity<Division>().HasData(
                new Division()
                {
                    Id = 1,
                    CreateDate = DateTime.Now,
                    Title = "Mecanica",
                    Description = "Activitate Principala",
                },

                new Division()
                {
                    Id = 2,
                    CreateDate = DateTime.Now,
                    Title = "Vulcanizare",
                    Description = "Activitate Secundara"
                });

            modelBuilder.Entity<ServiceType>().HasData(
                new ServiceType()
                {
                    DevisionId = 1,
                    Id = 1,
                    Title = "Directie",
                    Description = "Dir 1"
                },
                new ServiceType()
                {
                    DevisionId = 1,
                    Id = 2,
                    Title = "Motor",
                    Description = "Mot 1"
                },
                new ServiceType()
                {
                    DevisionId = 2,
                    Id = 3,
                    Title = "Roti",
                    Description = "Rot 1"
                },
                new ServiceType()
                {
                    DevisionId = 2,
                    Id = 4,
                    Title = "Valve",
                    Description = "Val 1"
                });
           modelBuilder.Entity<Service>().HasData(
                        new Service() { ServiceTypeId =4, Id = 13, Title = "Valva metal", Price = 90, UM = "BUC" },
                        new Service() { ServiceTypeId = 4, Id = 14, Title = "Valva cu surub", Price = 10, UM = "BUC" },
                        new Service() { ServiceTypeId = 4, Id = 15, Title = "Valva Sensor", Price = 105, UM = "BUC" },
                        new Service() { ServiceTypeId = 4, Id = 16, Title = "Valva simpla", Price = 20, UM = "BUC" },
                        new Service() { ServiceTypeId = 3, Id = 9, Title = "Resapate 19", Price = 156, UM = "BUC" },
                        new Service() { ServiceTypeId = 3, Id = 10, Title = "Vara-Iarna 20", Price = 150, UM = "BUC" },
                        new Service() { ServiceTypeId = 3, Id = 11, Title = "Motocicleta 14", Price = 155, UM = "BUC" },
                        new Service() { ServiceTypeId = 3, Id = 12, Title = "Tractor T4", Price = 520, UM = "BUC" },
                        new Service() { ServiceTypeId = 2, Id = 5, Title = "Capac Ulei", Price = 90, UM = "BUC" },
                        new Service() { ServiceTypeId = 2, Id = 6, Title = "Filtru Aer", Price = 50, UM = "BUC" },
                        new Service() { ServiceTypeId = 2, Id = 7, Title = "Semering", Price = 105, UM = "BUC" },
                        new Service() { ServiceTypeId = 2, Id = 8, Title = "Vibrochen", Price = 220, UM = "BUC" },
                        new Service() { ServiceTypeId = 1, Id = 1, Title = "Bucsa", Price = 56, UM = "BUC" },
                        new Service() { ServiceTypeId = 1, Id = 2, Title = "Planetara", Price = 50, UM = "BUC" },
                        new Service() { ServiceTypeId = 1, Id = 3, Title = "Caseta de directie", Price = 155, UM = "BUC" },
                        new Service() { ServiceTypeId = 1, Id = 4, Title = "Etrier", Price = 20, UM = "BUC" }
                );



            modelBuilder.Entity<Site>().HasData(new Site()
            {
                Id = 1,
                Title = "Timisoara 1",
                CompanyId = 1,
                BillDetailsId = 1,
            });

            modelBuilder.Entity<SiteDivision>().HasData(
                new SiteDivision() { SiteId = 1, DivisionId = 1 },
                    new SiteDivision() { SiteId = 1, DivisionId = 2 });
            modelBuilder.Entity<SiteUser>().HasData( new SiteUser() { SiteId = 1, UserId = 1 });
            modelBuilder.Entity<Company>().HasData(
                new Company() { 
                    Id = 1,
                    Title = "REMPA PRIME SRL",
                    CIF = "47829189",
                    RegCom = "J35/1134/2023",
                    Address = "Serata 90 Slova, Jud.Timis",
                    Phone = "0744566843",
                    SocialCapital = "200RON",
                    Email = "emailtest@email.ts",
                    Bank = "ING Bank",
                    IBAN = "RO34ING34426FF45234264",
                    SiteId = 1,
                    });
            modelBuilder.Entity<BillDetails>().HasData(
                new BillDetails()
                {
                    Id = 1,
                    SiteId = 1,
                    BillSerie = "CHITM2024",
                    BillNr = 1,
                    DevizNr = 1,
                    DevizSerie = "DEVTM2024",
                    InvoiceNr = 1,
                    InvoiceSerie ="FACTM2024",
                    Title ="Inital",

                });
            ;


            base.OnModelCreating(modelBuilder);
        }
    }
}
