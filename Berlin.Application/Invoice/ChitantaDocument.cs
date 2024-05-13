using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Berlin.Application.Invoice
{
    public class ChitantaDocument : IDocument
    {
        public InvoiceModel Model { get; }

        public ChitantaDocument(InvoiceModel model)
        {
            Model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

      public void Compose(IDocumentContainer container)
    {
            container
                 .Page(page =>
                 {
                     page.Size(PageSizes.A4);
                     page.Margin(25);
                     page.Content().Column(column =>
                     {
                         var totalPrice = Model.Items.Sum(x => x.Price * x.Count);
                         BuildChitanta(column, totalPrice, Model);
                         column.Spacing(30);
                         column.Item().AlignCenter().Text("--------------------------------------------------------"
                                                            +" Generata cu sistemul Berlin de la Professor Prime " +
                                                            "--------------------------------------------------------").FontSize(8);
                         column.Spacing(30);

                         BuildChitanta(column, totalPrice, Model);

                     });


                     var path = Path.Combine(Model.WebRootPath, Model.BillDetails.BackgroundURL);
                     var image = InvoiceDocument.LoadImageWithTransparency(new FileStream(path, FileMode.Open, FileAccess.Read), 0.05f);
                     if (image != null)
                     {
                         page.Background().Image(image);
                     }
                 });
        }

        private static void BuildChitanta(ColumnDescriptor column, float price, InvoiceModel Model)
        {
            var Address = Model.SellerAddress;
            var firstPart = Math.Floor(price);
            double fractionalPart = (price - Math.Floor(price))*100;
            column.Item().Container().Height(12, Unit.Centimetre).Border(1).Padding(20).Row(row =>
            {
                row.RelativeItem().Column(col => {
                    col.Item().Row(ro =>
                    {
                        ro.RelativeColumn().Column(col =>
                        {
                            col.Item().Text($"Furnizor: {Address.Title}");
                            col.Item().Text($"Reg.com: {Address.RegCom}");
                            col.Item().Text($"CIF: {Address.CIF}");
                            col.Item().Text($"Adresa: {Address.Address}");
                        });
                        var path = Path.Combine(Model.WebRootPath, Model.BillDetails.LogoURL);
                        ro.ConstantItem(80).Image(path);

                    });
                    col.Item().Row(ro =>
                    {
                        ro.RelativeColumn().AlignCenter().Column(col =>
                        {
                            col.Item().AlignCenter().Text("Chitanță", TextStyle.Default.Size(20).Bold());
                            col.Item().Container().Height(2, Unit.Centimetre).Width(6, Unit.Centimetre).Padding(10).Border(1).Row(r => {
                                r.RelativeColumn().AlignCenter().Column(c =>
                                {
                                    c.Item().Text($"Data: {Model.Receipt.Bill.CreateDate:d}");
                                    c.Item().Text($"Serie: {Model.Receipt.Bill.Title} NR.{Model.Receipt.Bill.Description}");
                                }); });
                        });
                    });
                    col.Item().Row(ro =>
                    {
                        var pretInCifre = fractionalPart > 0 ? $"{NumbersToWords.Convert((int)firstPart)} virgula  {NumbersToWords.Convert((int)fractionalPart)}" : NumbersToWords.Convert((int)firstPart);

                        var regCom = string.IsNullOrEmpty(Model.Receipt.ClientDetails.RegCom) ? ".....x....." : Model.Receipt.ClientDetails.RegCom;
                        var address = string.IsNullOrEmpty(Model.Receipt.ClientDetails.Address) ? ".....x....." : Model.Receipt.ClientDetails.Address;
                        var invoice = Model.Receipt.Invoice == null ? " " : $"Reprezentand contravaloarea facturii cu seria {Model.Receipt.Invoice.Title} nr.{Model.Receipt.Invoice.Description}.";
                        var cif = string.IsNullOrEmpty(Model.Receipt.ClientDetails.CIF) ? ".....x....." : Model.Receipt.ClientDetails.CIF;


                        ro.RelativeColumn(3).Stack(stack =>
                        {
                            stack.Item().Text($"       Am primit de la : {Model.Receipt.ClientDetails.Title}, CIF: {cif}, "
                                            + $"Reg.com: {regCom}, Adresa: {address} suma de "
                                            + $"{ price} RON, adică {pretInCifre} RON. {invoice}");
                            stack.Item().AlignRight().Text("\nCasier: __________________");
                        });
                    });
                });
            });
        }
    }
}
