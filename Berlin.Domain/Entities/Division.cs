using System.ComponentModel.DataAnnotations.Schema;

namespace Berlin.Domain.Entities
{
    public class Division : BaseDbObject
    {
        public ICollection<SiteDivision> Sites { get; set; } = new List<SiteDivision>();

        public ICollection<ServiceType> ServiceTypes { get; set; } = new List<ServiceType>();

    }
}

