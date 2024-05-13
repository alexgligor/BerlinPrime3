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
        public float Total { get; set; }
        public int SiteId { get; set; }

        public Site Site { get; set; }

        public int? BillId { get; set; }

        public Bill Bill { get; set; }

        public int? DevizId { get; set; }

        public Deviz Deviz { get; set; }

        public int? InvoiceId { get; set; }

        public Invoice Invoice { get; set; }

        public PayMethod PayMethod { get; set; } = PayMethod.NotPayed;

        public CompanyDetails ClientDetails { get; set; }
        public int ClientDetailsId { get; set; }

    }
}
