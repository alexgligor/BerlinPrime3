using Berlin.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Berlin.Application.Invoice
{
    public static class DocumentGenerator
    {
        public static byte[] Invoice(List<SelledService> selledServices, Receipt receipt, Company company)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var model = new InvoiceModel();
            model.Items = selledServices;
            model.Receipt = receipt;
            model.SellerAddress = company;
            var document = new InvoiceDocument(model);
            var gen = Document.Create(container => {
                document.Compose(container);
            });
            return gen
                .GeneratePdf();
        }

        public static byte[] Deviz(List<SelledService> selledServices, Receipt receipt, Company company)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var model = new InvoiceModel();
            model.Items = selledServices;
            model.Receipt = receipt;
            model.SellerAddress = company;
            var document = new InvoiceDocument(model) { IsInvoice = false};
            var gen = Document.Create(container => {
                document.Compose(container);
            });
            return gen
                .GeneratePdf();
        }

        public static byte[] Chitanta(List<SelledService> selledServices, Receipt receipt, Company company)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var model = new InvoiceModel();
            model.Items = selledServices;
            model.Receipt = receipt;
            model.SellerAddress = company;
            var document = new ChitantaDocument(model);
            var gen = Document.Create(container => {
                document.Compose(container);
            });
            return gen
                .GeneratePdf();
        }
    }
}
