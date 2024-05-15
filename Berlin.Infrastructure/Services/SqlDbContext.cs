using Berlin.Domain.Entities;
using Berlin.Domain.Entities.ProductManagement;
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
        public DbSet<ProductHistory> ProductHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SelledService>()
                .HasOne(ab => ab.Receipt)
                .WithMany(a => a.SelledServices)
                .HasForeignKey(ab => ab.ReceiptId);

            modelBuilder.Entity<ProductSite>()
                .HasKey(ps => new { ps.SiteId, ps.ProductId });

            modelBuilder.Entity<ProductSite>()
                .HasOne(ps => ps.Product)
                .WithMany(p => p.SiteProducts)
                .HasForeignKey(ps => ps.ProductId);

            modelBuilder.Entity<ProductSite>()
                .HasOne(ps => ps.Site)
                .WithMany(s => s.Products)
                .HasForeignKey(ps => ps.SiteId);

            modelBuilder.Entity<ProductService>()
                .HasKey(ps => new { ps.ServiceId, ps.ProductId });

            modelBuilder.Entity<ProductService>()
                .HasOne(ps => ps.Service)
                .WithMany(s => s.Products)
                .HasForeignKey(ps => ps.ServiceId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductService>()
                .HasOne(ps => ps.Product)
                .WithMany(p => p.Services)
                .HasForeignKey(ps => ps.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductHistores)
                .WithOne(ph => ph.Product)
                .HasForeignKey(ph => ph.ProductId);

            modelBuilder.Entity<Company>()
                .HasOne(c => c.Site)
                .WithOne(s => s.Company)
                .HasForeignKey<Site>(s => s.CompanyId);

            modelBuilder.Entity<CompanyDetails>()
                .HasOne(cd => cd.Receipt)
                .WithOne(r => r.ClientDetails)
                .HasForeignKey<Receipt>(r => r.ClientDetailsId);

            modelBuilder.Entity<BillDetails>()
                .HasOne(bd => bd.Site)
                .WithOne(s => s.BillDetails)
                .HasForeignKey<Site>(s => s.BillDetailsId);

            modelBuilder.Entity<Bill>()
                .HasOne(b => b.Receipt)
                .WithOne(r => r.Bill)
                .HasForeignKey<Receipt>(r => r.BillId);

            modelBuilder.Entity<Deviz>()
                .HasOne(d => d.Receipt)
                .WithOne(r => r.Deviz)
                .HasForeignKey<Receipt>(r => r.DevizId);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Receipt)
                .WithOne(r => r.Invoice)
                .HasForeignKey<Receipt>(r => r.InvoiceId);

            modelBuilder.Entity<SiteUser>()
                .HasKey(su => new { su.SiteId, su.UserId });

            modelBuilder.Entity<SiteUser>()
                .HasOne(su => su.Site)
                .WithMany(s => s.Users)
                .HasForeignKey(su => su.SiteId);

            modelBuilder.Entity<SiteUser>()
                .HasOne(su => su.User)
                .WithMany(u => u.Sites)
                .HasForeignKey(su => su.UserId);

            modelBuilder.Entity<Site>()
                .HasMany(s => s.Devices)
                .WithOne(d => d.Site)
                .HasForeignKey(d => d.SiteId);

            modelBuilder.Entity<Site>()
                .HasMany(s => s.ProductHistories)
                .WithOne(d => d.Site)
                .HasForeignKey(d => d.SiteId);

            modelBuilder.Entity<Division>()
                .HasMany(d => d.ServiceTypes)
                .WithOne(st => st.Devision)
                .HasForeignKey(st => st.DevisionId);

            modelBuilder.Entity<ServiceType>()
                .HasMany(st => st.Services)
                .WithOne(s => s.ServiceType)
                .HasForeignKey(s => s.ServiceTypeId);

            modelBuilder.Entity<SiteDivision>()
                .HasKey(sd => new { sd.DivisionId, sd.SiteId });

            modelBuilder.Entity<SiteDivision>()
                .HasOne(sd => sd.Site)
                .WithMany(s => s.Divisions)
                .HasForeignKey(sd => sd.SiteId);

            modelBuilder.Entity<SiteDivision>()
                .HasOne(sd => sd.Division)
                .WithMany(d => d.Sites)
                .HasForeignKey(sd => sd.DivisionId);

            // Seed Data for Entities
            modelBuilder.Entity<User>().HasData(
                new User() { Id = 1, Title = "Alex", Description = "Administrator" },
                new User() { Id = 2, Title = "ELF", Description = "Utilizator" }
            );

            modelBuilder.Entity<Division>().HasData(
                new Division() { Id = 1, CreateDate = DateTime.Now, Title = "Mecanica", Description = "Activitate Principala" },
                new Division() { Id = 2, CreateDate = DateTime.Now, Title = "Vulcanizare", Description = "Activitate Secundara" }
            );

            modelBuilder.Entity<ServiceType>().HasData(
                new ServiceType() { DevisionId = 1, Id = 1, Title = "Directie", Description = "Dir 1" },
                new ServiceType() { DevisionId = 1, Id = 2, Title = "Motor", Description = "Mot 1" },
                new ServiceType() { DevisionId = 2, Id = 3, Title = "Roti", Description = "Rot 1" },
                new ServiceType() { DevisionId = 2, Id = 4, Title = "Valve", Description = "Val 1" }
            );

            modelBuilder.Entity<Service>().HasData(
                new Service() { ServiceTypeId = 4, Id = 13, Title = "Valva metal", Price = 90, UM = "BUC" },
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

            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 1, Title = "Oring Z", SiteId = 1, CreateDate = DateTime.Now.AddDays(-20), Count = 30, Price = 10.0f },
                new Product() { Id = 2, Title = "Faseta", SiteId = 1, CreateDate = DateTime.Now.AddDays(-42), Count = 150, Price = 20.0f }
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
                new SiteDivision() { SiteId = 1, DivisionId = 2 }
            );

            modelBuilder.Entity<SiteUser>().HasData(
                new SiteUser() { SiteId = 1, UserId = 1 }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company()
                {
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
                }
            );

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
                    InvoiceSerie = "FACTM2024",
                    Title = "Inital",
                }
            );

            modelBuilder.Entity<ProductService>().HasData(
                new ProductService() { ProductId = 1, ServiceId = 1 },
                new ProductService() { ProductId = 1, ServiceId = 2 },
                new ProductService() { ProductId = 2, ServiceId = 3 },
                new ProductService() { ProductId = 2, ServiceId = 4 }
            );

            modelBuilder.Entity<ProductSite>().HasData(
                new ProductSite() { ProductId = 1, SiteId = 1, Count = 10 },
                new ProductSite() { ProductId = 2, SiteId = 1, Count = 20 }
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
