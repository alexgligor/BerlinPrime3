using Berlin.Domain.Entities;
using Berlin.Domain.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Berlin.Infrastructure.Services
{
    public class ReportsDataService : IReportsDataService
    {
        private readonly SqlDbContext _context;

        public ReportsDataService(SqlDbContext context)
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
                .Where(ss => ss.UserId == userId & !ss.IsDeleted)
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

        public async Task<List<(User User, float TargetValue)>> GetCurrentMonthTargetsForAllUsersAsync()
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var users = await _context.Users
                .Include(u => u.Sites)
                .ToListAsync();

            var userTargets = new List<(User User, float TargetValue)>();

            foreach (var user in users)
            {
                if (user.Target == null)
                {
                    continue;
                }

                var totalSales = await _context.SelledServices
                    .Where(ss => ss.UserId == user.Id && ss.Receipt.CreateDate.Year == currentYear && ss.Receipt.CreateDate.Month == currentMonth)
                    .SumAsync(ss => ss.Price * ss.Count);

                var targetValue = ((float)(totalSales / user.Target * 100));
                userTargets.Add((user, targetValue));
            }

            return userTargets;
        }

        public async Task<List<(Service service, float totalAmount, float percentage)>> GetServiceSumsByPeriodAsync(int siteId, DateTime startDate, DateTime endDate)
        {
            var result = await _context.SelledServices
                .Include(ss => ss.Service)
                .Include(ss => ss.Receipt)
                .Where(ss => ss.Receipt.SiteId == siteId && ss.CreateDate >= startDate && ss.CreateDate <= endDate)
                .GroupBy(ss => ss.Service)
                .Select(group => new
                {
                    Service = group.Key,
                    TotalAmount = group.Sum(ss => ss.Price * ss.Count)
                })
                .ToListAsync();

            float totalAmount = result.Sum(g => g.TotalAmount);

            return result
                .Select(g => (
                    service: g.Service,
                    totalAmount: g.TotalAmount,
                    percentage: (totalAmount == 0) ? 0 : (g.TotalAmount / totalAmount) * 100
                ))
                .ToList();
        }

        public async Task<List<(ServiceType serviceType, float totalAmount, float percentage)>> GetServiceTypeSumsByPeriodAsync(int siteId, DateTime startDate, DateTime endDate)
        {
            var result = await _context.SelledServices
                .Include(ss => ss.Service)
                    .ThenInclude(s => s.ServiceType)
                .Include(ss => ss.Receipt)
                .Where(ss => ss.Receipt.SiteId == siteId && ss.CreateDate >= startDate && ss.CreateDate <= endDate)
                .GroupBy(ss => ss.Service.ServiceType)
                .Select(group => new
                {
                    ServiceType = group.Key,
                    TotalAmount = group.Sum(ss => ss.Price * ss.Count)
                })
                .ToListAsync();

            float totalAmount = result.Sum(g => g.TotalAmount);

            return result
                .Select(g => (
                    serviceType: g.ServiceType,
                    totalAmount: g.TotalAmount,
                    percentage: (totalAmount == 0) ? 0 : (g.TotalAmount / totalAmount) * 100
                ))
                .ToList();
        }

        public async Task<List<UserPerformance>> GetMonthlySumsByUserAndPeriodAsync(DateTime startDate, DateTime endDate)
        {
            var result = await _context.SelledServices
                .Include(ss => ss.User)
                .Where(ss => ss.CreateDate >= startDate && ss.CreateDate <= endDate)
                .GroupBy(ss => ss.User)
                .Select(userGroup => new
                {
                    User = userGroup.Key,
                    MonthlySums = userGroup
                        .GroupBy(ss => new { Year = ss.CreateDate.Year, Month = ss.CreateDate.Month })
                        .Select(monthGroup => new
                        {
                            Month = new DateTime(monthGroup.Key.Year, monthGroup.Key.Month, 1).ToString("yyyy-MM"),
                            TotalAmount = monthGroup.Sum(ss => ss.Price * ss.Count)
                        })
                        .ToList()
                })
                .ToListAsync();

            return result
                .Select(user => new UserPerformance()
                {
                    User = user.User,
                    DateTotal = user.MonthlySums
                        .Select(m => new DateTotal() { month = m.Month, totalAmount = m.TotalAmount })
                        .ToList()
                })
                .ToList();
        }


    }

}
