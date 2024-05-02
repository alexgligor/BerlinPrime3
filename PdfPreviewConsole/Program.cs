using Berlin.Application.Invoice;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Diagnostics;

var filePath = "invoice2.pdf";

DocumentGenerator.Invoice(filePath);
Process.Start("explorer.exe", filePath);