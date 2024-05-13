using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SkiaSharp;
using Image = QuestPDF.Infrastructure.Image;
using QRCoder;
using System.Drawing;

namespace Berlin.Application.Invoice
{
    public class InvoiceDocument : IDocument
    {
        public InvoiceModel Model { get; }

        public bool IsInvoice { get; set; } = true;

        public InvoiceDocument(InvoiceModel model)
        {
            Model = model;
        }


        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);

                    page.Footer().Row(row =>
                    {
                        row.RelativeItem().AlignCenter().AlignBottom().Text(x =>
                        {
                            x.CurrentPageNumber();
                            x.Span(" / ");
                            x.TotalPages();
                        });
                        var qrcode = IsInvoice ? Model.Receipt.Invoice.QRLink : Model.Receipt.Deviz.QRLink;
                        if (string.IsNullOrEmpty(qrcode))
                            qrcode = "www.google.com";
                        row.ConstantItem(60).AlignRight().Image(GenerateQRCodeAsStream(qrcode));
                    });

                    if (!string.IsNullOrEmpty(Model.BillDetails.BackgroundURL))
                    {
                        var path = Path.Combine(Model.WebRootPath , Model.BillDetails.BackgroundURL);
                        var image = LoadImageWithTransparency(new FileStream(path, FileMode.Open, FileAccess.Read), 0.05f);
                        if (image != null)
                        {
                            page.Background().Image(image);
                        }
                    }

                });
        }

        void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text(IsInvoice ? "Factură" : "Deviz").Style(titleStyle);

                    if (IsInvoice)
                    {
                        column.Item().Text($"{Model.Receipt.Invoice.Title} #{Model.Receipt.Invoice.Description}").Style(titleStyle).FontSize(10);
                    }
                    else
                    {
                        column.Item().Text($"{Model.Receipt.Deviz.Title} #{Model.Receipt.Deviz.Description}").Style(titleStyle).FontSize(10);
                    }


                    var date = IsInvoice ? Model.Receipt.Invoice.CreateDate: Model.Receipt.Deviz.CreateDate;
                    column.Item().Text(text =>
                    {
                        text.Span("Dată: ").SemiBold();
                        text.Span($"{date:d}");
                    });

                    if(IsInvoice)
                        column.Item().Text(text =>
                        {
                            text.Span("Data scadentă: ").SemiBold();
                            text.Span($"{date.AddDays(Model.BillDetails.PayDays):d}");
                        });
                });

                if (!string.IsNullOrEmpty(Model.BillDetails.BackgroundURL))
                {
                    var path = Path.Combine(Model.WebRootPath , Model.BillDetails.LogoURL);

                    row.ConstantItem(80).Image(path);
                }
            });
        }

        void ComposeContent(IContainer container)
        {
            container.Column(column =>
            {
                column.Spacing(5);

                column.Item().Row(row =>
                {
                    var r1 = new CompanyCompose("Furnizor", Model.SellerAddress);
                    
                    r1.Compose(row);
                    row.ConstantItem(50);
                    if (IsInvoice)
                    {                         
                        var r2 = new AddressComponent("Cumpărător", Model.Receipt.ClientDetails);
                        r2.ComposeShort(row);
                    }
                    else
                    {
                        row.RelativeItem().Column(column =>
                        {
                            column.Spacing(1);

                            column.Item().BorderBottom(1).PaddingBottom(5).Text("Detalii Client").SemiBold();

                            column.Item().Text(Model.Receipt.Title).SemiBold();
                        });
                    }
                });

                column.Item().Element(ComposeTable);

                var totalPrice = Model.Items.Sum(x => x.Price * x.Count);
                var TVA = (double)totalPrice * 0.19;
                var TotalNet = totalPrice - TVA;
                column.Item().AlignRight().Text($"Total NET: {TotalNet.ToString("0.00")} RON").FontSize(10);
                column.Item().AlignRight().Text($"Total TVA: {TVA.ToString("0.00")} RON").FontSize(10);
                column.Item().AlignRight().Text($"Total: {totalPrice.ToString("0.00")} RON").FontSize(14).Bold();
                column.Item().PaddingTop(20).Row(c =>
                {
                    c.RelativeItem().AlignLeft().Text($"Semnatura primire: ............").FontSize(12).SemiBold();
                    c.RelativeItem().AlignCenter().Text($"Semnatura furnizor: ............").FontSize(12).SemiBold();
                });

                
                column.Item().PaddingTop(5).Element(ComposeComments);
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
                    columns.RelativeColumn(2);
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
                    table.Cell().Element(CellStyle).Text(item.Service.Title);
                    table.Cell().Element(CellStyle).AlignCenter().Text(item.Count);
                    table.Cell().Element(CellStyle).Text(item.Service.UM);
                    var itemTVA = (double)item.Price * 0.19;
                    var itemNet = item.Price - itemTVA;
                    table.Cell().Element(CellStyle).AlignCenter().Text(itemNet.ToString("0.00"));
                    var sumPriceNet = itemNet * item.Count;
                    table.Cell().Element(CellStyle).AlignCenter().Text($"{sumPriceNet.ToString("0.00")}");
                    var tva = (double)itemTVA * item.Count;
                    var sum = sumPriceNet + tva;
                    table.Cell().Element(CellStyle).Text($"{tva.ToString("0.00")}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"{sum.ToString("0.00")}");
                    static IContainer CellStyle(IContainer container)
                    {
                        return container.DefaultTextStyle(x => x.FontSize(9)).BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                    }
                }
            });
        }

        void ComposeComments(IContainer container)
        {
            var disclamer = IsInvoice ? Model.Receipt.Invoice.Disclamer : Model.Receipt.Deviz.Disclamer;
            if (!string.IsNullOrWhiteSpace(disclamer))
                container.PaddingVertical(20).Column(column =>
                {
                    column.Spacing(5);

                    column.Item().Row(row =>
                    {

                        row.RelativeItem(120).Column(column =>
                        {

                            column.Item().PaddingBottom(5).Text("Info").SemiBold();

                            column.Item().Text(disclamer).FontSize(6);
                        });
                    });
                });
        }

        public static Image LoadImageWithTransparency(FileStream fileStream, float transparency)
        {
            try
            {
                using var originalImage = SKImage.FromEncodedData(fileStream);

                using var surface = SKSurface.Create(originalImage.Width, originalImage.Height, SKColorType.Rgba8888, SKAlphaType.Premul);
                using var canvas = surface.Canvas;

                using var transparencyPaint = new SKPaint
                {
                    ColorFilter = SKColorFilter.CreateBlendMode(SKColors.White.WithAlpha((byte)(transparency * 255)), SKBlendMode.DstIn)
                };

                canvas.DrawImage(originalImage, new SKPoint(0, 0), transparencyPaint);

                var encodedImage = surface.Snapshot().Encode(SKEncodedImageFormat.Png, 100).ToArray();
                return Image.FromBinaryData(encodedImage);
            }
            catch(Exception ex) {
                Console.WriteLine("Unable to process the background image!",ex.Message);
            }
            return null;
        }
        public static FileStream GenerateQRCodeAsStream(string content)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
             
            using (var qrCode = new QRCode(qrCodeData))
            {
                using (Bitmap qrCodeImage = qrCode.GetGraphic(10))
                {
                    string tempFileName = Path.GetTempFileName();
                    qrCodeImage.Save(tempFileName, System.Drawing.Imaging.ImageFormat.Png);

                    return new FileStream(tempFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                }
            }
        }

    }
}
