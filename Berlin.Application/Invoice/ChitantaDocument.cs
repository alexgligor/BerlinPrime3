using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
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
                         column.Spacing(10); // spațiu între chitanțe

                         var totalPrice = Model.Items.Sum(x => x.Price * x.Count);
                         BuildChitanta(column, totalPrice);
                         column.Spacing(70);
                         BuildChitanta(column, totalPrice);

                     });

                     var image = InvoiceDocument.LoadImageWithTransparency(new FileStream(@"C:\Users\dan-alexandru.gligor\Downloads\lg.png", FileMode.Open, FileAccess.Read), 0.05f);
                     if (image != null)
                     {
                         page.Background().Image(image);
                     }
                 });
        }

        private static void BuildChitanta(ColumnDescriptor column, float price)
        {
            var firstPart = Math.Floor(price);
            double fractionalPart = (price - Math.Floor(price))*100;
            column.Item().Container().Height(5, Unit.Centimetre).Border(1).Padding(10).Row(row =>
            {
                row.RelativeColumn(3).Stack(stack =>
                {
                    stack.Item().Text("Chitanță", TextStyle.Default.Size(20).Bold());
                    stack.Item().Text($"Data: {DateTime.Now.ToString("dd/MM/yyyy")}");
                    stack.Item().Text($"Sumă: {price} RON");
                    if(fractionalPart>0)
                        stack.Item().Text($"Sumă in cifre: {NumbersToWords.Convert((int) firstPart)} virgula  {NumbersToWords.Convert((int)fractionalPart)} RON").SemiBold();
                    else
                        stack.Item().Text($"Sumă in cifre: {NumbersToWords.Convert((int) firstPart)} RON").SemiBold();
                    stack.Item().Text("Primit de la: Ion Popescu");
                    stack.Item().Text("Serviciu: Consultație");
                    stack.Item().Text("Semnătură: __________________");
                });

                row.ConstantItem(80).AlignRight().AlignTop().Image(@"C:\Users\dan-alexandru.gligor\Downloads\ROFESSOr.png"); // înlocuiește "path_to_your_image.jpg" cu calea reală către imaginea ta
            });
        }
    }
}
