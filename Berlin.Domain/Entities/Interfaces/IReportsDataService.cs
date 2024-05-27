namespace Berlin.Domain.Entities.Interfaces
{
    public interface IReportsDataService
    {
        Task<float> GetCurrentMonthTargetPercentageAsync(int userId);
        Task<List<float>> GetMonthlyTargetValuesAsync(int userId);
        Task<List<(User User, float TargetValue)>> GetCurrentMonthTargetsForAllUsersAsync();
        Task<List<(Service service, float totalAmount, float percentage)>> GetServiceSumsByPeriodAsync(int siteId, DateTime startDate, DateTime endDate);
        Task<List<(ServiceType serviceType, float totalAmount, float percentage)>> GetServiceTypeSumsByPeriodAsync(int siteId, DateTime startDate, DateTime endDate);

        Task<List<UserPerformance>> GetMonthlySumsByUserAndPeriodAsync(DateTime startDate, DateTime endDate);
    }

    public class DateTotal
    {
        public string month;
        public float totalAmount;
    }

    public class UserPerformance
    {

        public List<DateTotal> DateTotal;
        public User User;

    }


}
