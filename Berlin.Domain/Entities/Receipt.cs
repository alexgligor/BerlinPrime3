using Berlin.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Domain.Entities
{
    public class Receipt:BaseDbObject
    {
        public int SiteId { get; set; }

        public Site Site { get; set; }

        public PayMethod PayMethod { get; set; } = PayMethod.NotPayed;
    }
}
