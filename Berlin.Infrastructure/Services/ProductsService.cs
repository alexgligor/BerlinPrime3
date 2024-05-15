using Berlin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using System.Linq.Expressions;

namespace Berlin.Infrastructure.Services
{
    public class ProductsService : IProductService
    {
        private readonly SqlDbContext _db;

        public ProductsService(SqlDbContext db)
        {
            _db = db;
            _db.Database.EnsureCreated();
        }

        public  Product GetWithListsMembers(int id)
        {
            return   _db.Products
             .Include(p => p.Services)
             .ThenInclude(c => c.Service)
             .Include(p => p.Service)
             .FirstOrDefault(p => p.Id == id);
        }
    }
}
