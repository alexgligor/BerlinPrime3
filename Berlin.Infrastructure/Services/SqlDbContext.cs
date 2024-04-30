using Berlin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Berlin.Infrastructure.Services
{
    public class SqlDbContext: DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options)
       : base(options)
        {
            
        }
        public DbSet<GenericArticle> GenericArticles{ get; set; }
        public DbSet<Division> Divisions{ get; set; }
        public DbSet<ServiceType> ServiceTypes{ get; set; }
        public DbSet<SiteDivision> SiteDivisions{ get; set; }
        public DbSet<SiteUser> SiteUsers { get; set; }
        public DbSet<Device> Devices{ get; set; }
        public DbSet<Site> Sites{ get; set; }
        public DbSet<User> Users{ get; set; }
        public DbSet<SelledService> SelledServices { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                new GenericArticle { Location ="Building A" ,Id = 1, Title = "Surub", Count = 100, Currency="RON", Price=2, Description="16/8", CreateDate=DateTime.Now},
                new GenericArticle { Location = "Building B", Id = 2, Title = "Ruleta", Count = 80, Currency="RON", Price=17, Description="5m,magnetica", CreateDate=DateTime.Now},
                new GenericArticle { Location = "Building A", Id = 3, Title = "Echer", Count = 60, Currency="RON", Price=6, Description="40/40", CreateDate=DateTime.Now},
                new GenericArticle { Location = "Magazin", Id = 4, Title = "Dalta 20", Count = 120, Currency="RON", Price=2, Description="Maner de cauciuc", CreateDate=DateTime.Now},
                new GenericArticle { Location = "Magazin", Id = 5, Title = "Drujba Hyundai", Count = 3, Currency="RON", Price=690, Description="2CP", CreateDate=DateTime.Now}
            );

            modelBuilder.Entity<User>().HasData(
            new User() { Id = 1, Title = "Alex", Description = "Administrator"}
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
