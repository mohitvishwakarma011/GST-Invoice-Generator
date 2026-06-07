using GI.Application.DataTransferObjects.InvoiceWorkItem;
using MediatR;

namespace GI.Application.Features.InvoiceWorkItem.Queries.GetInvoicesForUser
{
    public class GetInvoicesForUserQuery : IRequest<IList<InvoiceListDto>>
    {
        public int UserId { get; set; }
    }
}
