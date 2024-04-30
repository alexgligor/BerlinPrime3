using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Domain.Entities
{
    public class User:BaseDbObject
    {     
        public string? ImageUrl { get; set; }

        public ICollection<SiteUser> Sites { get; set; } = new List<SiteUser>();

    }
}
