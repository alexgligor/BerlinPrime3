using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Domain.Entities
{
    public class Site: BaseDbObject
    {
        public ICollection<SiteDivision> Divisions { get; set; } = new List<SiteDivision>();
        public ICollection<Device> Devices { get; set; } = new List<Device>();
        public ICollection<SiteUser> Users { get; set; } = new List<SiteUser>();
    }
}
