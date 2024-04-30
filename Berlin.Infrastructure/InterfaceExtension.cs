using Berlin.Domain.Entities;

namespace Berlin.Infrastructure
{
    public static class InterfaceExtension
    {
        public static async Task<IEnumerable<Site>> GetAllWithRelations(this IGenericService<Site> service)
        {
            return await service.GetAll(s => s.Divisions, s => s.Devices, s => s.Users, s => s.Users);
        }

        public static async Task<IEnumerable<Site>> GetAllWithAllDivisions(this IGenericService<Site> service)
        {
            return await service.GetAll(s => s.Divisions);
        }

        public static async Task<IEnumerable<Site>> GetAllWithAllDevices(this IGenericService<Site> service)
        {
            return await service.GetAll(s => s.Devices);
        }

        public static async Task<IEnumerable<Site>> GetAllWithAllUsers(this IGenericService<Site> service)
        {
            return await service.GetAll(s => s.Users);
        }


        public static async Task<Division> LoadRelations(this IGenericService<Division> service, int id)
        {
            return await service.Find(id,s => s.ServiceTypes);
        }

        public static async Task<ServiceType> LoadRelations(this IGenericService<ServiceType> service, int id)
        {
            return await service.Find(id, s => s.Services);
        }
    }
}
