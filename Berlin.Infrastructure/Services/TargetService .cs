using Berlin.Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Berlin.Infrastructure.Services
{
    public class TargetService : ITargetService
    {
        private readonly SqlDbContext _context;

        public TargetService(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<float> GetCurrentMonthTargetPercentageAsync(int userId)
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var user = await _context.Users
                .Include(u => u.Sites)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Target == null)
            {
                throw new ArgumentException("User not found or target not set.");
            }

            var totalSales = await _context.SelledServices
                .Where(ss => ss.UserId == userId && ss.Receipt.CreateDate.Year == currentYear && ss.Receipt.CreateDate.Month == currentMonth)
                .SumAsync(ss => ss.Price * ss.Count);

            return (float)(totalSales / user.Target * 100);
        }

        public async Task<List<float>> GetMonthlyTargetValuesAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Sites)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Target == null)
            {
                throw new ArgumentException("User not found or target not set.");
            }

            var monthlyTargets = await _context.SelledServices
                .Where(ss => ss.UserId == userId)
                .GroupBy(ss => new { ss.Receipt.CreateDate.Year, ss.Receipt.CreateDate.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalSales = g.Sum(ss => ss.Price * ss.Count)
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

            return monthlyTargets.Select(mt => (float)(mt.TotalSales / user.Target * 100)).ToList();
        }
    }

}
