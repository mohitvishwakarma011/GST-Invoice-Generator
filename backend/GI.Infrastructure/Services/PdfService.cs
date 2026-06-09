using GI.Application.Common.Interfaces;
using GI.Application.DataTransferObjects.InvoiceWorkItem;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace GI.Infrastructure.Services
{
    public class PdfService : IPdfService
    {
        public byte[] GenerateInvoicePdf(InvoiceDetailForPdfDto invoice)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Element(header => ComposeHeader(header, invoice));
                    page.Content().Element(content => ComposeContent(content, invoice));
                    page.Footer().Element(footer => ComposeFooter(footer, invoice));
                });
            }).GeneratePdf();
        }

        private static void ComposeHeader(IContainer container, InvoiceDetailForPdfDto invoice)
        {
            container.Column(col =>
            {
                col.Item().Row(row =>
                {
                    // Left — your business details
                    row.RelativeItem().Column(left =>
                    {
                        left.Item().Text(invoice.User.BusinessName)
                            .Bold().FontSize(16);

                        left.Item().Text(invoice.User.Address)
                            .FontSize(9).FontColor(Colors.Grey.Darken2);

                        left.Item().Text($"GSTIN: {invoice.User.Gstin}")
                            .FontSize(9);

                        left.Item().Text(invoice.User.Email)
                            .FontSize(9).FontColor(Colors.Grey.Darken2);

                        left.Item().Text($"State: {invoice.User.State} ({invoice.User.StateCode:D2})")
                            .FontSize(9).FontColor(Colors.Grey.Darken2);
                    });

                    // Right — TAX INVOICE label + invoice meta
                    row.RelativeItem().AlignRight().Column(right =>
                    {
                        right.Item().Text("TAX INVOICE")
                            .Bold().FontSize(20).FontColor(Colors.Blue.Darken2);

                        right.Item().PaddingTop(4).Text($"# {invoice.InvoiceNumber}")
                            .Bold().FontSize(12);

                        right.Item().Text($"Date: {invoice.CreatedOn:dd MMM yyyy}")
                            .FontSize(9).FontColor(Colors.Grey.Darken2);

                        right.Item().Text($"Due: {invoice.DueDate:dd MMM yyyy}")
                            .FontSize(9).FontColor(Colors.Grey.Darken2);

                        right.Item().Text($"Status: {invoice.Status}")
                            .Bold().FontSize(9);
                    });
                });

                // Divider below header
                col.Item().PaddingTop(12).LineHorizontal(1)
                    .LineColor(Colors.Blue.Darken2);
            });
        }
        private static void ComposeContent(IContainer container, InvoiceDetailForPdfDto invoice)
        {
            container.Column(col =>
            {
                col.Spacing(16);

                // Bill To
                col.Item().PaddingTop(8).Row(row =>
                {
                    row.RelativeItem().Column(billTo =>
                    {
                        billTo.Item().Text("BILL TO")
                            .Bold().FontSize(9).FontColor(Colors.Grey.Darken2);

                        billTo.Item().Text(invoice.Client.Name)
                            .Bold().FontSize(12);

                        billTo.Item().Text(invoice.Client.BillingAddress)
                            .FontSize(9).FontColor(Colors.Grey.Darken2);

                        if (!string.IsNullOrEmpty(invoice.Client.Gstin))
                            billTo.Item().Text($"GSTIN: {invoice.Client.Gstin}")
                                .FontSize(9);

                        billTo.Item().Text($"State: {invoice.Client.State} ({invoice.Client.StateCode:D2})")
                            .FontSize(9).FontColor(Colors.Grey.Darken2);

                        billTo.Item().Text(invoice.Client.Email)
                            .FontSize(9).FontColor(Colors.Grey.Darken2);
                    });
                });

                // Items Table
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(25);   // #
                        columns.RelativeColumn(4);    // Description
                        columns.RelativeColumn(2);    // HSN/SAC
                        columns.ConstantColumn(40);   // Qty
                        columns.RelativeColumn(2);    // Rate
                        columns.RelativeColumn(2);    // Amount
                    });

                    // Table header row
                    table.Header(header =>
                    {
                        static IContainer HeaderCell(IContainer c) =>
                            c.Background(Colors.Blue.Darken2)
                             .PaddingVertical(6)
                             .PaddingHorizontal(4)
                             .AlignMiddle();

                        header.Cell().Element(HeaderCell)
                            .Text("#").FontSize(9).Bold().FontColor(Colors.White);
                        header.Cell().Element(HeaderCell)
                            .Text("Description").FontSize(9).Bold().FontColor(Colors.White);
                        header.Cell().Element(HeaderCell)
                            .Text("HSN/SAC").FontSize(9).Bold().FontColor(Colors.White);
                        header.Cell().Element(HeaderCell).AlignCenter()
                            .Text("Qty").FontSize(9).Bold().FontColor(Colors.White);
                        header.Cell().Element(HeaderCell).AlignRight()
                            .Text("Rate").FontSize(9).Bold().FontColor(Colors.White);
                        header.Cell().Element(HeaderCell).AlignRight()
                            .Text("Amount").FontSize(9).Bold().FontColor(Colors.White);
                    });

                    // Item rows
                    var index = 1;
                    foreach (var item in invoice.Items)
                    {
                        var bg = index % 2 == 0 ? Colors.Grey.Lighten4 : Colors.White;

                        IContainer RowCell(IContainer c) =>
                            c.Background(bg)
                             .PaddingVertical(5)
                             .PaddingHorizontal(4)
                             .AlignMiddle();

                        table.Cell().Element(RowCell)
                            .Text(index.ToString()).FontSize(9);
                        table.Cell().Element(RowCell)
                            .Text(item.Description).FontSize(9);
                        table.Cell().Element(RowCell)
                            .Text(item.HsnCode ?? "-").FontSize(9);
                        table.Cell().Element(RowCell).AlignCenter()
                            .Text(item.Quantity.ToString("G29")).FontSize(9);
                        table.Cell().Element(RowCell).AlignRight()
                            .Text($"₹{item.Rate:N2}").FontSize(9);
                        table.Cell().Element(RowCell).AlignRight()
                            .Text($"₹{item.Amount:N2}").FontSize(9);

                        index++;
                    }
                });

                // Totals
                col.Item().AlignRight().Column(totals =>
                {
                    totals.Spacing(4);

                    // Subtotal row
                    totals.Item().Row(row =>
                    {
                        row.RelativeItem().AlignRight()
                            .Text("Subtotal:").FontSize(10);
                        row.ConstantItem(110).AlignRight()
                            .Text($"₹{invoice.Subtotal:N2}").FontSize(10);
                    });

                    // GST rows — show conditionally
                    if (invoice.Cgst > 0)
                    {
                        totals.Item().Row(row =>
                        {
                            row.RelativeItem().AlignRight().Text("CGST (9%):").FontSize(10);
                            row.ConstantItem(110).AlignRight().Text($"₹{invoice.Cgst:N2}").FontSize(10);
                        });
                        totals.Item().Row(row =>
                        {
                            row.RelativeItem().AlignRight().Text("SGST (9%):").FontSize(10);
                            row.ConstantItem(110).AlignRight().Text($"₹{invoice.Sgst:N2}").FontSize(10);
                        });
                    }
                    else
                    {
                        totals.Item().Row(row =>
                        {
                            row.RelativeItem().AlignRight().Text("IGST (18%):").FontSize(10);
                            row.ConstantItem(110).AlignRight().Text($"₹{invoice.Igst:N2}").FontSize(10);
                        });
                    }

                    // Total row
                    totals.Item().PaddingTop(4).LineHorizontal(1)
                        .LineColor(Colors.Grey.Lighten2);

                    totals.Item().Row(row =>
                    {
                        row.RelativeItem().AlignRight()
                            .Text("Total:").Bold().FontSize(12);
                        row.ConstantItem(110).AlignRight()
                            .Text($"₹{invoice.Total:N2}")
                            .Bold().FontSize(12).FontColor(Colors.Blue.Darken2);
                    });
                });
            });
        }

        private static void ComposeFooter(IContainer container, InvoiceDetailForPdfDto invoice)
        {
            container.Column(col =>
            {
                col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                col.Item().PaddingTop(8).Row(row =>
                {
                    // Bank details
                    row.RelativeItem().Column(bank =>
                    {
                        bank.Item().Text("PAYMENT DETAILS")
                            .Bold().FontSize(9).FontColor(Colors.Grey.Darken2);

                        if (!string.IsNullOrEmpty(invoice.User.BankName))
                            bank.Item().Text($"Bank: {invoice.User.BankName}").FontSize(9);

                        if (!string.IsNullOrEmpty(invoice.User.AccountNumber))
                            bank.Item().Text($"A/C: {invoice.User.AccountNumber}").FontSize(9);

                        if (!string.IsNullOrEmpty(invoice.User.IfscCode))
                            bank.Item().Text($"IFSC: {invoice.User.IfscCode}").FontSize(9);

                        if (!string.IsNullOrEmpty(invoice.User.UpiId))
                            bank.Item().Text($"UPI: {invoice.User.UpiId}").FontSize(9);
                    });

                    // Notes
                    if (!string.IsNullOrEmpty(invoice.Notes))
                    {
                        row.RelativeItem().Column(notes =>
                        {
                            notes.Item().Text("NOTES")
                                .Bold().FontSize(9).FontColor(Colors.Grey.Darken2);
                            notes.Item().Text(invoice.Notes)
                                .FontSize(9).FontColor(Colors.Grey.Darken2);
                        });
                    }
                });

                // Thank you note
                col.Item().PaddingTop(8).AlignCenter()
                    .Text("Thank you for your business!")
                    .FontSize(9).Italic().FontColor(Colors.Grey.Darken1);
            });
        }
    }
}