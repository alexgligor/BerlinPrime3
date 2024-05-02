using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Application.Invoice
{
    public class AddressComponent : IComponent
    {
        private string Title { get; }
        private CompanyDetails Address { get; }

        public AddressComponent(string title, CompanyDetails address)
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

                column.Item().Text(Address.CompanyName);
                column.Item().Text(Address.Address);
                column.Item().Text(Address.CIF);
                column.Item().Text(Address.Bank);
                column.Item().Text(Address.IBAN);
                column.Item().Text(Address.Email);
                column.Item().Text(Address.Phone);
                column.Item().Text(Address.SocialCapital);
            });
        }

        public void ComposeShort(RowDescriptor row)
        {
            row.RelativeItem().Column(column =>
            {
                column.Spacing(1);

                column.Item().BorderBottom(1).PaddingBottom(5).Text(Title).SemiBold();

                column.Item().Text(Address.CompanyName).SemiBold();
                column.Item().Text(Address.CIF);
                column.Item().Text($"Delegat: {Address.Delegate}");
                column.Item().Text($"Detalii: {Address.Comments}");
            });
        }
    }
}
