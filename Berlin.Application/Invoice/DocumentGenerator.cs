using Berlin.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Berlin.Application.Invoice
{
    public static class DocumentGenerator
    {
        public static void Invoice(string path)
        {
            CreateInvoiceDocument().GeneratePdf(path);
        }

        public static void Deviz(string path)
        {
            CreateInvoiceDocument(false).GeneratePdf(path);
        }

        private static Document CreateInvoiceDocument(bool isInvoice = true)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var model = InvoiceDocumentDataSource.GetInvoiceDetails();
            var document = new InvoiceDocument(model) { IsInvoice = isInvoice};
            var gen = Document.Create(container => {
                document.Compose(container);
            });
            return gen;
        }
        public static byte[] Invoice()
        { 
            return CreateInvoiceDocument().GeneratePdf();
        }

        public static byte[] Invoice(List<SelledService> selledServices, Receipt receipt)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var model = InvoiceDocumentDataSource.GetInvoiceDetails();
            model.Items = selledServices;
            model.Receipt = receipt;
            var document = new InvoiceDocument(model);
            var gen = Document.Create(container => {
                document.Compose(container);
            });
            return gen
                .GeneratePdf();
        }

        public static byte[] Deviz(List<SelledService> selledServices, Receipt receipt)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var model = InvoiceDocumentDataSource.GetInvoiceDetails();
            model.Items = selledServices;
            model.Receipt = receipt;
            var document = new InvoiceDocument(model) { IsInvoice = false};
            var gen = Document.Create(container => {
                document.Compose(container);
            });
            return gen
                .GeneratePdf();
        }

        public static byte[] Chitanta(List<SelledService> selledServices, Receipt receipt)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var model = InvoiceDocumentDataSource.GetInvoiceDetails();
            model.Items = selledServices;
            model.Receipt = receipt;
            var document = new ChitantaDocument(model);
            var gen = Document.Create(container => {
                document.Compose(container);
            });
            return gen
                .GeneratePdf();
        }
    }
}
