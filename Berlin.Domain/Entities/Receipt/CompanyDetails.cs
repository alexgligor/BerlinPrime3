using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Domain.Entities
{
    public class CompanyDetails : BaseDbObject
    {
        public string? Address { get; set; }
        public string? CIF { get; set; }
        public string? Bank { get; set; }
        public string? IBAN { get; set; }
        public string? RegCom { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? SocialCapital { get; set; }
        public string? Delegate { get; set; }
        public string? Comments { get; set; }

        public int ReceiptId { get; set; }

        public Receipt Receipt { get; set; }
    }
}
