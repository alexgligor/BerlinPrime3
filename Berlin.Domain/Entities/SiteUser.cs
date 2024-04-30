using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Domain.Entities
{
    public class SiteUser
    {
        public User User { get; set; }
        public int UserId { get; set; }

        public Site Site { get; set; }
        public int SiteId { get; set; } 
    }
}
