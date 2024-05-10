using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Domain.Entities
{
    public class ServiceType:BaseDbObject
    {
        public int DevisionId { get; set; }
        public Division Devision { get; set; }

        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
