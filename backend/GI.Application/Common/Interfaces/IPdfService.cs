using GI.Application.DataTransferObjects.InvoiceWorkItem;

namespace GI.Application.Common.Interfaces
{
    public interface IPdfService
    {
        byte[] GenerateInvoicePdf(InvoiceDetailForPdfDto invoice);
    }
}
