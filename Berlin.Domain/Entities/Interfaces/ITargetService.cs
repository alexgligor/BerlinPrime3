namespace Berlin.Domain.Entities.Interfaces
{
    public interface ITargetService
    {
        Task<float> GetCurrentMonthTargetPercentageAsync(int userId);
        Task<List<float>> GetMonthlyTargetValuesAsync(int userId);
    }

}
