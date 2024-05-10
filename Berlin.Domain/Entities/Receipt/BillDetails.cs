using Berlin.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Domain.Entities
{
    public class BillDetails:BaseDbObject
    {
        public int BillNr { get; set; }//chitanta
        public int DevizNr { get; set; }
        public int InvoiceNr { get; set; }//factura

        public string BillSerie { get; set; }
        public string DevizSerie { get; set; }
        public string InvoiceSerie { get; set; }

        public Site Site { get; set; }
        public int SiteId { get; set; }

        public void IncreaseBillNumber() => BillNr++;
        public void IncreaseInvoiceNumber() => InvoiceNr++;
        public void IncreaseDevizNumber() => DevizNr++;

    }
}
