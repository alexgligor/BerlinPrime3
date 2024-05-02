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

        private static Document CreateInvoiceDocument()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var model = InvoiceDocumentDataSource.GetInvoiceDetails();
            var document = new InvoiceDocument(model);
            var gen = Document.Create(container => {
                document.Compose(container);
            });
            return gen;
        }
        public static byte[] Invoice()
        { 
            return CreateInvoiceDocument().GeneratePdf();
        }
    }
}
