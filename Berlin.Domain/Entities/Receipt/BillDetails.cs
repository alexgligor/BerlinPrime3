namespace Berlin.Domain.Entities
{
    public class BillDetails:BaseDbObject
    {
        public int BillNr { get; set; }//chitanta
        public int DevizNr { get; set; }
        public int InvoiceNr { get; set; }//factura
        public int PayDays { get; set; }


        public string? BillSerie { get; set; }
        public string? DevizSerie { get; set; }
        public string? InvoiceSerie { get; set; }
        public string?  DevizDisclamer { get; set; }
        public string?  InvoiceDisclamer { get; set; }
        public string?  QRCodeLink { get; set; }
        public string?  LogoURL { get; set; }
        public string?  BackgroundURL { get; set; }

        public Site Site { get; set; }
        public int SiteId { get; set; }

        public void IncreaseBillNumber() => BillNr++;
        public void IncreaseInvoiceNumber() => InvoiceNr++;
        public void IncreaseDevizNumber() => DevizNr++;

    }
}
