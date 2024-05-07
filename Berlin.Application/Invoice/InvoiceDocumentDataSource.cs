using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berlin.Application.Invoice
{
    using Berlin.Domain.Entities;
    using QuestPDF.Helpers;

    public static class InvoiceDocumentDataSource
    {
        private static Random Random = new Random();

        public static InvoiceModel GetInvoiceDetails()
        {
            var items = Enumerable
                .Range(1, 8)
                .Select(i => GenerateRandomOrderItem())
                .ToList();

            return new InvoiceModel
            {
                InvoiceNumber = Random.Next(1_000, 10_000),
                IssueDate = DateTime.Now,
                DueDate = DateTime.Now + TimeSpan.FromDays(14),

                SellerAddress = GenerateRandomAddress(),
                CustomerAddress = GenerateRandomAddress(),

                Items = items,
                Comments = Placeholders.Paragraph()
            };
        }

        private static SelledService GenerateRandomOrderItem()
        {
            return new SelledService
            {
                Service = new Service() {
                    Title = Placeholders.Label(), UM = "BUC" },

                Title = Placeholders.Label(),
                Price = (float)Math.Round(Random.NextDouble() * 100, 2),
                Count = Random.Next(1, 10),
               
            };
        }

        private static CompanyDetails GenerateRandomAddress()
        {
            return new CompanyDetails
            {
                CompanyName = Placeholders.Name(),
                Address = Placeholders.Label(),
                Delegate = Placeholders.Label(),
                Bank = Placeholders.Label(),
                CIF = "SR345643",
                IBAN = "RO13BRD234554FR3456",
                Email = Placeholders.Email(),
                Phone = Placeholders.PhoneNumber(),
                Comments = Placeholders.Label(),
                SocialCapital = Placeholders.Integer(),

            };
        }
    }
}
