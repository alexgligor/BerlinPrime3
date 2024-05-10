using Berlin.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Application.Invoice
{
    public class CompanyCompose : IComponent
    {
        private string Title { get; }
        private Company Address { get; }

        public CompanyCompose(string title, Company address)
        {
            Title = title;
            Address = address;
        }

        public void Compose(RowDescriptor row)
        {
            row.RelativeItem().Column(column =>
            {
                column.Spacing(1);

                column.Item().BorderBottom(1).PaddingBottom(5).Text(Title).SemiBold();

                column.Item().Text(Address.Title).FontSize(10);
                column.Item().Text(Address.Address).FontSize(10);
                column.Item().Text(Address.CIF).FontSize(10);
                column.Item().Text(Address.Bank).FontSize(10);
                column.Item().Text(Address.IBAN).FontSize(10);
                column.Item().Text(Address.Email).FontSize(10);
                column.Item().Text(Address.Phone).FontSize(10);
                column.Item().Text(Address.SocialCapital).FontSize(10);
            });
        }

        public void ComposeShort(RowDescriptor row)
        {
            row.RelativeItem().Column(column =>
            {
                column.Spacing(1);

                column.Item().BorderBottom(1).PaddingBottom(5).Text(Title).SemiBold();

                column.Item().Text(Address.Title).SemiBold();
                column.Item().Text(Address.CIF);
                column.Item().Text($"Delegat: {Address.Delegate}");
                column.Item().Text($"Detalii: {Address.Comments}");
            });
        }
    }
}
