using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Domain.Entities
{
    public class SiteDivision
    {
        public Division Division { get; set; }
        public int DivisionId { get; set; }

        public Site Site { get; set; }
        public int SiteId { get; set; } 
    }
}
