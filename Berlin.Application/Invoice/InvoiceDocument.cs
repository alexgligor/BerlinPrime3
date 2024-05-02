using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace Berlin.Application.Invoice
{
    public class InvoiceDocument : IDocument
    {
        public InvoiceModel Model { get; }

        public InvoiceDocument(InvoiceModel model)
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
                    page.Margin(50);

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);


                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"Invoice #{Model.InvoiceNumber}").Style(titleStyle);

                    column.Item().Text(text =>
                    {
                        text.Span("Data curentă: ").SemiBold();
                        text.Span($"{Model.IssueDate:d}");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Data scadentă: ").SemiBold();
                        text.Span($"{Model.DueDate:d}");
                    });
                });

                row.ConstantItem(100).Height(50).Placeholder();
            });
        }

        void ComposeContent(IContainer container)
        {
            container.PaddingVertical(40).Column(column =>
            {
                column.Spacing(5);

                column.Item().Row(row =>
                {
                    var r1 = new AddressComponent("Vânzător", Model.SellerAddress);
                    var r2 = new AddressComponent("Cumpărător", Model.CustomerAddress);
                    r1.Compose(row);
                    row.ConstantItem(50);
                    r2.ComposeShort(row);
                });

                column.Item().Element(ComposeTable);

                var totalPrice = Model.Items.Sum(x => x.Price * x.Quantity);
                var total = (double)totalPrice* 0.19 + (double)totalPrice;
                column.Item().AlignRight().Text($"Total: {totalPrice.ToString("0.00")} RON").FontSize(14);

                if (!string.IsNullOrWhiteSpace(Model.Comments))
                    column.Item().PaddingTop(25).Element(ComposeComments);
            });
        }


        void ComposeTable(IContainer container)
        {
            container.Table(table =>
            {
                // step 1
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(20);
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                // step 2
                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("#");
                    header.Cell().Element(CellStyle).Text("Nume");
                    header.Cell().Element(CellStyle).Text("Cantitate");
                    header.Cell().Element(CellStyle).Text("U.M.");
                    header.Cell().Element(CellStyle).AlignCenter().Text("Preț Unitate");
                    header.Cell().Element(CellStyle).AlignCenter().Text("Preț");
                    header.Cell().Element(CellStyle).Text("TVA");
                    header.Cell().Element(CellStyle).AlignRight().Text("Total");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(3).BorderBottom(1).BorderColor(Colors.Black);
                    }
                });

                // step 3
                foreach (var item in Model.Items)
                {
                    table.Cell().Element(CellStyle).Text(Model.Items.IndexOf(item) + 1);
                    table.Cell().Element(CellStyle).Text(item.Name);
                    table.Cell().Element(CellStyle).AlignCenter().Text(item.Quantity);
                    table.Cell().Element(CellStyle).Text(item.UM);
                    table.Cell().Element(CellStyle).AlignCenter().Text(item.Price);
                    var sumPrice = item.Price * item.Quantity;
                    table.Cell().Element(CellStyle).AlignCenter().Text($"{sumPrice}");
                    var tva = (double)sumPrice * 0.19;
                    table.Cell().Element(CellStyle).Text($"{tva.ToString("0.00")}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"{(tva+(double)sumPrice).ToString("0.00")}");

                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.FontSize(9)).BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

        void ComposeComments(IContainer container)
        {
            container.Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
            {
                column.Spacing(5);
                column.Item().Text("Comentarii").FontSize(14);
                column.Item().Text(Model.Comments);
            });
        }
    }
}
