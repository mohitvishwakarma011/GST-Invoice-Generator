using GI.Application.DataTransferObjects.User;

namespace GI.Application.DataTransferObjects.InvoiceWorkItem
{
    public class InvoiceDetailForPdfDto : InvoiceDetailDto
    {
        public UserDetailDto User { get; set; } = null!;
    }
}
